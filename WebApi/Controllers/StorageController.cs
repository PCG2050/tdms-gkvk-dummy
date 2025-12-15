using Application.Interface;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StorageController : ControllerBase
    {
        private readonly IOrganizationService _organizationService;
        private readonly IAzureStorageService _azureStorageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ILogger<StorageController> _logger;

        public StorageController(
            IOrganizationService organizationService,
            IAzureStorageService azureStorageService,
            ICurrentUserService currentUserService,
            ILogger<StorageController> logger)
        {
            _organizationService = organizationService;
            _azureStorageService = azureStorageService;
            _currentUserService = currentUserService;
            _logger = logger;
        }

        [Authorize]
        [HttpPost("token")]
        public async Task<IActionResult> GenerateContainerToken(StorageTokenRequest request)
        {
            var role = _currentUserService.Role;
            var orgId = request.OrganizationId;
            if (role != Domain.Entities.Enum.Role.SUPERADMIN) orgId = _currentUserService.OrganizationId;
            
            var organization = await _organizationService.GetOrganizationAsync(orgId);
            if (organization is null) return NotFound("Organization not found");
            
            string containerName = request.IsPrivate ? organization.StorageContainerName : $"{organization.StorageContainerName}-public";
            var serviceResult = _azureStorageService.GenerateToken(containerName, role);
            
            if (!serviceResult.IsSuccess) 
                return ServiceResponseToActionResult.Error(serviceResult.ErrorMessage, serviceResult.ErrorStatus);
            
            return Ok(serviceResult.Data);
        }

        [Authorize]
        [HttpPost("upload-token/profile-image")]
        public async Task<IActionResult> GenerateProfileImageUploadToken([FromBody] ProfileImageUploadRequest request)
        {
            var userId = _currentUserService.UserId;
            var organizationId = _currentUserService.OrganizationId;

            // Validate file extension
            var allowedExtensions = new[] { "jpg", "jpeg", "png", "gif", "webp" };
            if (!allowedExtensions.Contains(request.FileExtension.ToLower()))
                return BadRequest("Invalid file extension. Allowed: jpg, jpeg, png, gif, webp");

            var result = _azureStorageService.GenerateProfileImageUploadToken(organizationId, userId, request.FileExtension);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return Ok(new
            {
                uploadUrl = result.Data.BlobUrl,
                sasToken = result.Data.SasToken,
                expiresAt = result.Data.ExpiresAt,
                blobName = _azureStorageService.GenerateProfileImageBlobName(userId, request.FileExtension)
            });
        }

        [Authorize]
        [HttpPost("upload-token/video")]
        public async Task<IActionResult> GenerateVideoUploadToken([FromBody] VideoUploadRequest request)
        {
            var userId = _currentUserService.UserId;
            var organizationId = _currentUserService.OrganizationId;

            // Validate file extension
            var allowedExtensions = new[] { "mp4", "avi", "mov", "wmv", "flv", "webm" };
            if (!allowedExtensions.Contains(request.FileExtension.ToLower()))
                return BadRequest("Invalid file extension. Allowed: mp4, avi, mov, wmv, flv, webm");

            var result = _azureStorageService.GenerateVideoUploadToken(organizationId, userId, request.FileExtension);

            if (!result.IsSuccess)
                return ServiceResponseToActionResult.Error(result.ErrorMessage, result.ErrorStatus);

            return Ok(new
            {
                uploadUrl = result.Data.BlobUrl,
                sasToken = result.Data.SasToken,
                expiresAt = result.Data.ExpiresAt,
                blobName = _azureStorageService.GenerateVideoBlobName(userId, "training", request.FileExtension)
            });
        }

        public class StorageTokenRequest
        {
            public int OrganizationId { get; set; }
            public bool IsPrivate { get; set; }
        }
        public class ProfileImageUploadRequest
        {
            public string FileExtension { get; set; }
        }

        public class VideoUploadRequest
        {
            public string FileExtension { get; set; }
            public string VideoType { get; set; } = "training";
        }

        // ========== NEW ENDPOINTS FOR DYNAMIC SAS TOKEN MANAGEMENT ==========

        /// <summary>
        /// Get a cached SAS token for a container (24-hour expiry with automatic caching)
        /// This replaces the need for hardcoded SAS tokens in the frontend
        /// </summary>
        [Authorize]
        [HttpPost("sas-token")]
        public async Task<IActionResult> GetSasToken([FromBody] GetSasTokenRequest request)
        {
            try
            {
                var organizationId = _currentUserService.OrganizationId;
                var organization = await _organizationService.GetOrganizationAsync(organizationId);

                if (organization == null)
                    return NotFound("Organization not found");

                // Determine container name based on isPrivate flag
                string containerName = request.IsPrivate
                    ? organization.StorageContainerName
                    : (organization.StorageContainerNamePublic ?? $"{organization.StorageContainerName}-public");

                // Get or generate SAS token (cached for 24 hours)
                var sasToken = await _azureStorageService.GetSasTokenAsync(
                    containerName,
                    !request.IsPrivate,
                    request.Permissions ?? "rwdl");

                return Ok(new
                {
                    sasToken = sasToken,
                    containerName = containerName,
                    accountName = "tdms", // TODO: Get from configuration
                    expiresInHours = 24,
                    note = "Token is cached and auto-renewed. Valid for 24 hours."
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate SAS token");
                return StatusCode(500, "Failed to generate SAS token");
            }
        }

        /// <summary>
        /// Upload a file to blob storage with user-based organization
        /// Path structure: {userId}/{folder}/{fileName}
        /// </summary>
        [Authorize]
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile([FromForm] FileUploadRequest request)
        {
            try
            {
                if (request.File == null || request.File.Length == 0)
                    return BadRequest("No file provided");

                var userId = _currentUserService.UserId;
                var organizationId = _currentUserService.OrganizationId;
                var organization = await _organizationService.GetOrganizationAsync(organizationId);

                if (organization == null)
                    return NotFound("Organization not found");

                // Determine container name
                string containerName = request.IsPrivate
                    ? organization.StorageContainerName
                    : (organization.StorageContainerNamePublic ?? $"{organization.StorageContainerName}-public");

                // Upload file
                using var stream = request.File.OpenReadStream();
                var blobUrl = await _azureStorageService.UploadFileAsync(
                    stream,
                    containerName,
                    userId,
                    request.File.FileName,
                    request.Folder);

                return Ok(new
                {
                    url = blobUrl,
                    fileName = request.File.FileName,
                    folder = request.Folder,
                    containerName = containerName
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload file");
                return StatusCode(500, "Failed to upload file");
            }
        }

        /// <summary>
        /// Delete a file from blob storage
        /// </summary>
        [Authorize]
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteFile([FromBody] DeleteFileRequest request)
        {
            try
            {
                var organizationId = _currentUserService.OrganizationId;
                var organization = await _organizationService.GetOrganizationAsync(organizationId);

                if (organization == null)
                    return NotFound("Organization not found");

                // Determine container name
                string containerName = request.IsPrivate
                    ? organization.StorageContainerName
                    : (organization.StorageContainerNamePublic ?? $"{organization.StorageContainerName}-public");

                var result = await _azureStorageService.DeleteFileAsync(containerName, request.BlobName);

                if (result)
                    return Ok(new { message = "File deleted successfully" });
                else
                    return NotFound("File not found");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete file");
                return StatusCode(500, "Failed to delete file");
            }
        }

        /// <summary>
        /// List files in user's folder
        /// </summary>
        [Authorize]
        [HttpGet("list")]
        public async Task<IActionResult> ListFiles([FromQuery] string folder = null, [FromQuery] bool isPrivate = false)
        {
            try
            {
                var userId = _currentUserService.UserId;
                var organizationId = _currentUserService.OrganizationId;
                var organization = await _organizationService.GetOrganizationAsync(organizationId);

                if (organization == null)
                    return NotFound("Organization not found");

                // Determine container name
                string containerName = isPrivate
                    ? organization.StorageContainerName
                    : (organization.StorageContainerNamePublic ?? $"{organization.StorageContainerName}-public");

                // Construct prefix: {userId}/{folder}/
                var prefix = folder != null
                    ? $"{userId}/{folder}/"
                    : $"{userId}/";

                var files = await _azureStorageService.ListFilesAsync(containerName, prefix);

                return Ok(new
                {
                    files = files,
                    count = files.Count,
                    containerName = containerName,
                    prefix = prefix
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to list files");
                return StatusCode(500, "Failed to list files");
            }
        }

        // ========== REQUEST/RESPONSE MODELS ==========

        public class GetSasTokenRequest
        {
            public bool IsPrivate { get; set; } = false;
            public string? Permissions { get; set; } = "rwdl"; // Read, Write, Delete, List
        }

        public class FileUploadRequest
        {
            public required IFormFile File { get; set; }
            public string? Folder { get; set; }
            public bool IsPrivate { get; set; } = false;
        }

        public class DeleteFileRequest
        {
            public required string BlobName { get; set; }
            public bool IsPrivate { get; set; } = false;
        }
    }
}

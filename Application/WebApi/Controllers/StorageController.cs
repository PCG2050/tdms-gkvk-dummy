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

        public StorageController(IOrganizationService organizationService, IAzureStorageService azureStorageService, ICurrentUserService currentUserService)
        {
            _organizationService = organizationService;
            _azureStorageService = azureStorageService;
            _currentUserService = currentUserService;
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
    }
}

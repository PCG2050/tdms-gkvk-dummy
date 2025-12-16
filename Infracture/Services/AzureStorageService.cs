using Application.Enums;
using Application.Interface;
using Application.Interface.Repository;
using Application.Interface.Services.Common;
using Application.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Domain.Entities;
using Domain.Entities.Enum;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Infrastructure.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        const int CONTAINER_NAME_MAX_LENGTH = 63;//DO NOT CHANGE, Max allowed container length 63
        int EXP_TIME_MIN = 60;
        const string CONTAINER_NAME_PATTERN = "^[a-z](?!.*--)[a-z0-9-]{1,61}[a-z0-9]$";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly ILogger<AzureStorageService> _logger;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly TdmsDbContext _context;
        private readonly string _connectionString;
        private readonly string _accountName;
        private readonly string _accountKey;
        private readonly int _sasTokenExpiryHours;

        public AzureStorageService(IConfiguration configuration,
            ILogger<AzureStorageService> logger,
            IOrganizationRepository organizationRepository,
            TdmsDbContext context)
        {
            _connectionString = configuration.GetConnectionString("AzureStorage");
            _blobServiceClient = new BlobServiceClient(_connectionString);
            _accountName = configuration["AzureStorage:AccountName"];
            _accountKey = configuration["AzureStorage:AccountKey"];
            _sasTokenExpiryHours = int.Parse(configuration["AzureStorage:SasTokenExpiryHours"] ?? "24");
            _logger = logger;
            _organizationRepository = organizationRepository;
            _context = context;
        }
        public async Task<bool> ContainerExistsAsync(string containerName)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var response = await containerClient.ExistsAsync();
                return response.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to check if container {containerName} exists");
                return false;
            }
        }


        public ServiceResult<AzureStorageSASResult> GenerateToken(string containerName, Role role)
        {
            try
            {
                var _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                if (!_blobContainerClient.CanGenerateSasUri) return ServiceResult<AzureStorageSASResult>.Failure($"Cannot generate SAS Uri for {containerName}");
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = _blobContainerClient.Name,
                    Resource = "c",
                    ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(EXP_TIME_MIN)
                };
                BlobContainerSasPermissions rolePermissions = GetRoleBasedPermissions(role);
                sasBuilder.SetPermissions(rolePermissions);
                var sasUri = _blobContainerClient.GenerateSasUri(sasBuilder);
                var result = new AzureStorageSASResult
                {
                    AccountName = _blobContainerClient.AccountName,
                    AccountUrl = $"{_blobContainerClient.Uri.Scheme}://{_blobContainerClient.Uri.Host}",
                    ContainerName = _blobContainerClient.Name,
                    ContainerUri = _blobContainerClient.Uri,
                    SASUri = sasUri,
                    SASToken = sasUri.Query.TrimStart('?'),
                    SASPermission = sasBuilder.Permissions,
                    SASExpire = sasBuilder.ExpiresOn
                };
                return ServiceResult<AzureStorageSASResult>.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AzureStorageSASResult.GenerateToken ");
                return ServiceResult<AzureStorageSASResult>.Failure("Failure to generate SAS Token");
            }
        }

        public BlobSasTokenResponse GenerateBlobSasTokenAsync(string containerName, BlobSasTokenRequest request)
        {
            try
            {
                var blobClient = _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(request.BlobName);

                if (!blobClient.CanGenerateSasUri)
                    throw new InvalidOperationException("Cannot generate SAS URI for this blob");

                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    BlobName = request.BlobName,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(request.ExpiryInMinutes)
                };

                // Set permissions based on operation type
                switch (request.OperationType)
                {
                    case BlobOperationType.Upload:
                        sasBuilder.SetPermissions(BlobSasPermissions.Create | BlobSasPermissions.Write);
                        break;
                    case BlobOperationType.Read:
                        sasBuilder.SetPermissions(BlobSasPermissions.Read);
                        break;
                    case BlobOperationType.Delete:
                        sasBuilder.SetPermissions(BlobSasPermissions.Delete);
                        break;
                    case BlobOperationType.ReadWrite:
                        sasBuilder.SetPermissions(BlobSasPermissions.Read | BlobSasPermissions.Write | BlobSasPermissions.Create);
                        break;
                }

                var sasUri = blobClient.GenerateSasUri(sasBuilder);

                return new BlobSasTokenResponse
                {
                    SasToken = sasUri.Query.TrimStart('?'),
                    BlobUrl = sasUri.ToString(),
                    ExpiresAt = sasBuilder.ExpiresOn.DateTime
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate blob SAS token for {ContainerName}/{BlobName}",
                    containerName, request.BlobName);
                throw;
            }
        }

        public async Task<string> UploadBlobAsync(string containerName, string blobName, Stream fileStream, string contentType)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(blobName);

                var blobUploadOptions = new BlobUploadOptions
                {
                    HttpHeaders = new BlobHttpHeaders
                    {
                        ContentType = contentType
                    }
                };

                await blobClient.UploadAsync(fileStream, blobUploadOptions);
                return blobClient.Uri.ToString();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to upload blob {BlobName} to container {ContainerName}",
                    blobName, containerName);
                throw;
            }
        }

        public async Task<bool> DeleteBlobAsync(string containerName, string blobName)
        {
            try
            {
                var blobClient = _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
                var response = await blobClient.DeleteIfExistsAsync();
                return response.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete blob {BlobName} from container {ContainerName}",
                    blobName, containerName);
                return false;
            }
        }
        public async Task<Stream> DownloadBlobAsync(string containerName, string blobName)
        {
            try
            {
                var blobClient = _blobServiceClient.GetBlobContainerClient(containerName).GetBlobClient(blobName);
                var response = await blobClient.DownloadStreamingAsync();
                return response.Value.Content;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to download blob {BlobName} from container {ContainerName}",
                    blobName, containerName);
                throw;
            }
        }
        public ServiceResult<BlobSasTokenResponse> GenerateProfileImageUploadToken(int organizationId, int userId, string fileExtension)
        {
            try
            {
                var organization = _organizationRepository.GetOrganizationAsync(organizationId).Result;
                if (organization == null)
                    return ServiceResult<BlobSasTokenResponse>.Failure("Organization not found");

                var containerName = $"{organization.StorageContainerName}-public";
                var blobName = GenerateProfileImageBlobName(userId, fileExtension);

                var request = new BlobSasTokenRequest
                {
                    ContainerName = containerName,
                    BlobName = blobName,
                    OperationType = BlobOperationType.Upload,
                    ExpiryInMinutes = 30
                };

                var token = GenerateBlobSasTokenAsync(containerName, request);
                return ServiceResult<BlobSasTokenResponse>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate profile image upload token for user {UserId}", userId);
                return ServiceResult<BlobSasTokenResponse>.Failure("Failed to generate upload token");
            }
        }

        public ServiceResult<BlobSasTokenResponse> GenerateVideoUploadToken(int organizationId, int userId, string fileExtension)
        {
            try
            {
                var organization = _organizationRepository.GetOrganizationAsync(organizationId).Result;
                if (organization == null)
                    return ServiceResult<BlobSasTokenResponse>.Failure("Organization not found");

                var containerName = $"{organization.StorageContainerName}-public";
                var blobName = GenerateVideoBlobName(userId, "training", fileExtension);

                var request = new BlobSasTokenRequest
                {
                    ContainerName = containerName,
                    BlobName = blobName,
                    OperationType = BlobOperationType.Upload,
                    ExpiryInMinutes = 60
                };

                var token = GenerateBlobSasTokenAsync(containerName, request);
                return ServiceResult<BlobSasTokenResponse>.Success(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate video upload token for user {UserId}", userId);
                return ServiceResult<BlobSasTokenResponse>.Failure("Failed to generate upload token");
            }
        }

        public string GenerateProfileImageBlobName(int userId, string fileExtension)
        {
            var timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMdd_HHmmss");
            return $"profiles/{userId}/profile_{timestamp}.{fileExtension.TrimStart('.')}";
        }

        public string GenerateVideoBlobName(int userId, string videoType, string fileExtension)
        {
            var timestamp = DateTimeOffset.UtcNow.ToString("yyyyMMdd_HHmmss");
            return $"videos/{userId}/{videoType}/{videoType}_{timestamp}.{fileExtension.TrimStart('.')}";
        }


        private BlobContainerSasPermissions GetRoleBasedPermissions(Role role)
        {
            // VALID PERMISSION ORDER => racwdxltmeop
            switch (role)
            {
                case Role.SUPERADMIN:
                case Role.ADMIN:
                    return BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write | BlobContainerSasPermissions.Delete;
                case Role.UNITHEAD:
                case Role.TRAINER:
                    return BlobContainerSasPermissions.Read | BlobContainerSasPermissions.Write | BlobContainerSasPermissions.Delete;
                default:
                    return BlobContainerSasPermissions.Read;
            }
        }


        public async Task<ServiceResult> CreateStorageContainer(string containerName, ContainerType containerType)
        {
            var _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            //check if container already exists
            var operationInfo = new ContainerOperationInfo();
            string error = GetContainerNameErrors(_blobContainerClient.Name);
            if (error != null) return ServiceResult.Failure(error);
            if (await _blobContainerClient.ExistsAsync()) return ServiceResult.Failure("Container already exists");
            var accessLevel = containerType == ContainerType.Private ? PublicAccessType.None : PublicAccessType.Blob;
            await _blobContainerClient.CreateAsync(accessLevel);
            return ServiceResult.Success();

        }
        readonly static Regex _validContainerPattern = new Regex(CONTAINER_NAME_PATTERN);
        public string GetContainerNameErrors(string containerName)
        {
            if (string.IsNullOrWhiteSpace(containerName))
            {
                return "Container name is required";
            }
            containerName = containerName.Trim();
            if (containerName.Length < 3 || containerName.Length > CONTAINER_NAME_MAX_LENGTH)
            {
                return "Container name should be of length between 3 and 63";
            }
            if (!_validContainerPattern.IsMatch(containerName)) return "Container name pattern is not valid";

            return null;
        }

        // ========== NEW METHODS FOR DYNAMIC SAS TOKEN MANAGEMENT ==========

        /// <summary>
        /// Create both private and public containers for an organization
        /// </summary>
        public async Task<(bool Success, string PrivateContainer, string PublicContainer)>
            CreateOrganizationContainersAsync(
                int organizationId,
                string privateContainerName,
                string publicContainerName)
        {
            try
            {
                _logger.LogInformation(
                    "📦 Creating containers for organization {OrgId}: Private={Private}, Public={Public}",
                    organizationId, privateContainerName, publicContainerName);

                // Validate container names
                if (GetContainerNameErrors(privateContainerName) != null || GetContainerNameErrors(publicContainerName) != null)
                {
                    _logger.LogError("❌ Invalid container names provided");
                    return (false, null, null);
                }

                // Create private container (no public access)
                var privateResult = await CreateStorageContainer(privateContainerName, ContainerType.Private);
                if (!privateResult.IsSuccess && !await ContainerExistsAsync(privateContainerName))
                {
                    _logger.LogError("❌ Failed to create private container: {Error}", privateResult.ErrorMessage);
                    return (false, null, null);
                }

                // Create public container (blob-level public access)
                var publicResult = await CreateStorageContainer(publicContainerName, ContainerType.Public);
                if (!publicResult.IsSuccess && !await ContainerExistsAsync(publicContainerName))
                {
                    _logger.LogError("❌ Failed to create public container: {Error}", publicResult.ErrorMessage);
                    return (false, null, null);
                }

                _logger.LogInformation("✅ Containers created successfully for organization {OrgId}", organizationId);
                return (true, privateContainerName, publicContainerName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error creating containers for organization {OrgId}", organizationId);
                return (false, null, null);
            }
        }

        /// <summary>
        /// Get a SAS token for a container (with caching and on-demand cleanup)
        /// NO BACKGROUND SERVICE REQUIRED - Cleanup happens on-demand
        /// </summary>
        public async Task<string> GetSasTokenAsync(string containerName, bool isPublic, string permissions)
        {
            try
            {
                // 🔥 ON-DEMAND CLEANUP: Delete expired tokens (no background service needed)
                await CleanupExpiredTokensAsync();

                // Check cache first
                var cachedToken = await _context.SasTokenCache
                    .Where(t => t.ContainerName == containerName
                        && t.Permissions == permissions
                        && t.IsPublicContainer == isPublic
                        && t.ExpiresOn > DateTime.UtcNow.AddHours(1)) // At least 1 hour left
                    .OrderByDescending(t => t.ExpiresOn)
                    .FirstOrDefaultAsync();

                if (cachedToken != null)
                {
                    _logger.LogInformation("✅ Using cached SAS token for container {Container}", containerName);
                    return cachedToken.SasToken;
                }

                // Generate new token
                _logger.LogInformation("🔑 Generating new SAS token for container {Container}", containerName);

                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                // Build SAS token
                var sasBuilder = new BlobSasBuilder
                {
                    BlobContainerName = containerName,
                    Resource = "c", // Container
                    StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
                    ExpiresOn = DateTimeOffset.UtcNow.AddHours(_sasTokenExpiryHours)
                };

                // Parse permissions string (e.g., "rwdl")
                // replace the failing line with:
                var permissionFlags = default(Azure.Storage.Sas.BlobContainerSasPermissions);
                if (permissions.Contains('r')) permissionFlags |= BlobContainerSasPermissions.Read;
                if (permissions.Contains('w')) permissionFlags |= BlobContainerSasPermissions.Write;
                if (permissions.Contains('d')) permissionFlags |= BlobContainerSasPermissions.Delete;
                if (permissions.Contains('l')) permissionFlags |= BlobContainerSasPermissions.List;
                if (permissions.Contains('a')) permissionFlags |= BlobContainerSasPermissions.Add;
                if (permissions.Contains('c')) permissionFlags |= BlobContainerSasPermissions.Create;

                sasBuilder.SetPermissions(permissionFlags);

                var sasToken = sasBuilder.ToSasQueryParameters(
                    new Azure.Storage.StorageSharedKeyCredential(_accountName, _accountKey)
                ).ToString();

                // Cache the token
                var cacheEntry = new SasTokenCache
                {
                    ContainerName = containerName,
                    SasToken = sasToken,
                    Permissions = permissions,
                    ExpiresOn = sasBuilder.ExpiresOn.UtcDateTime,
                    IsPublicContainer = isPublic,
                    CreatedAt = DateTimeOffset.UtcNow
                };

                _context.SasTokenCache.Add(cacheEntry);
                await _context.SaveChangesAsync();

                _logger.LogInformation("💾 SAS token cached for container {Container}, expires at {ExpiresOn}",
                    containerName, sasBuilder.ExpiresOn);

                return sasToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error generating SAS token for container {Container}", containerName);
                throw;
            }
        }

        /// <summary>
        /// Upload a file to blob storage organized by userId
        /// Path structure: {userId}/{folder}/{fileName}
        /// </summary>
        public async Task<string> UploadFileAsync(
            Stream fileStream,
            string containerName,
            int userId,
            string fileName,
            string folder = null)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                // Construct blob path: {userId}/{folder}/{fileName}
                var blobPath = folder != null
                    ? $"{userId}/{folder}/{fileName}"
                    : $"{userId}/{fileName}";

                var blobClient = containerClient.GetBlobClient(blobPath);

                _logger.LogInformation(
                    "📤 Uploading file {FileName} to container {Container} at path {Path}",
                    fileName, containerName, blobPath);

                await blobClient.UploadAsync(fileStream, overwrite: true);

                // Return URL without SAS token
                var blobUrl = blobClient.Uri.ToString();
                _logger.LogInformation("✅ File uploaded successfully: {Url}", blobUrl);

                return blobUrl;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error uploading file {FileName} to container {Container}",
                    fileName, containerName);
                throw;
            }
        }

        /// <summary>
        /// Delete a file from blob storage
        /// </summary>
        public async Task<bool> DeleteFileAsync(string containerName, string blobName)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
                var blobClient = containerClient.GetBlobClient(blobName);

                var result = await blobClient.DeleteIfExistsAsync();

                if (result.Value)
                {
                    _logger.LogInformation("🗑️ Deleted blob {BlobName} from container {Container}",
                        blobName, containerName);
                }
                else
                {
                    _logger.LogWarning("⚠️ Blob {BlobName} not found in container {Container}",
                        blobName, containerName);
                }

                return result.Value;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error deleting blob {BlobName} from container {Container}",
                    blobName, containerName);
                return false;
            }
        }

        /// <summary>
        /// List files in a container with a prefix (e.g., all files for a userId)
        /// </summary>
        public async Task<List<string>> ListFilesAsync(string containerName, string prefix)
        {
            try
            {
                var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var blobs = new List<string>();

                await foreach (var blobItem in containerClient.GetBlobsAsync(prefix: prefix))
                {
                    blobs.Add(blobItem.Name);
                }

                _logger.LogInformation("📁 Listed {Count} files in container {Container} with prefix {Prefix}",
                    blobs.Count, containerName, prefix);

                return blobs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error listing files in container {Container} with prefix {Prefix}",
                    containerName, prefix);
                return new List<string>();
            }
        }

        /// <summary>
        /// ON-DEMAND CLEANUP: Delete expired SAS tokens from cache
        /// This runs during token generation instead of as a background service
        /// Perfect for basic tier Azure subscriptions (no additional cost)
        /// </summary>
        private async Task CleanupExpiredTokensAsync()
        {
            try
            {
                var expiredTokens = await _context.SasTokenCache
                    .Where(t => t.ExpiresOn < DateTime.UtcNow)
                    .ToListAsync();

                if (expiredTokens.Any())
                {
                    _logger.LogInformation("🧹 Cleaning up {Count} expired SAS tokens", expiredTokens.Count);
                    _context.SasTokenCache.RemoveRange(expiredTokens);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "⚠️ Error during SAS token cleanup (non-critical)");
                // Don't throw - cleanup failure shouldn't break token generation
            }
        }
    }
}

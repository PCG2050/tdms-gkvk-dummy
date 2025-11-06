using Application.Enums;
using Application.Interface;
using Application.Interface.Repository;
using Application.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;
using Domain.Entities.Enum;
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

        public AzureStorageService(IConfiguration configuration, 
            ILogger<AzureStorageService> logger,
            IOrganizationRepository organizationRepository)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
            _logger = logger;   
            _organizationRepository = organizationRepository;
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
                _logger.LogError(ex,"AzureStorageSASResult.GenerateToken ");
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


        public async Task<ServiceResult> CreateStorageContainer(string containerName,ContainerType containerType)
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
    }
}

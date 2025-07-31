using Application.Enums;
using Application.Interface;
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

        public AzureStorageService(IConfiguration configuration, ILogger<AzureStorageService> logger)
        {
            var connectionString = configuration.GetConnectionString("AzureStorage");
            _blobServiceClient = new BlobServiceClient(connectionString);
            _logger = logger;   
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
                return null;
            }
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

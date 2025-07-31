using Application.Enums;
using Application.Models;
using Domain.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IAzureStorageService
    {
        //Task<ServiceResult> CreateContainersAsync(string containerName);
        //BlobSasTokenResponse GenerateBlobSasTokenAsync(string organizationId, BlobSasTokenRequest request);
        //Task<string> UploadBlobAsync(string containerName, string blobName, Stream fileStream, string contentType);
        //Task<bool> DeleteBlobAsync(string containerName, string blobName);
        //Task<Stream> DownloadBlobAsync(string containerName, string blobName);
        Task<bool> ContainerExistsAsync(string containerName);
        //OLD
        ServiceResult<AzureStorageSASResult> GenerateToken(string containerName, Role role);
        Task<ServiceResult> CreateStorageContainer(string containerName, ContainerType containerType);
        string GetContainerNameErrors(string containerName);
    }
}

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
        // ========== EXISTING METHODS ==========
        Task<bool> ContainerExistsAsync(string containerName);
        ServiceResult<AzureStorageSASResult> GenerateToken(string containerName, Role role);
        Task<ServiceResult> CreateStorageContainer(string containerName, ContainerType containerType);
        string GetContainerNameErrors(string containerName);

        // Blob-level SAS token methods
        BlobSasTokenResponse GenerateBlobSasTokenAsync(string containerName, BlobSasTokenRequest request);
        Task<string> UploadBlobAsync(string containerName, string blobName, Stream fileStream, string contentType);
        Task<bool> DeleteBlobAsync(string containerName, string blobName);
        Task<Stream> DownloadBlobAsync(string containerName, string blobName);

        // Helper methods for profile management
        ServiceResult<BlobSasTokenResponse> GenerateProfileImageUploadToken(int organizationId, int userId, string fileExtension);
        ServiceResult<BlobSasTokenResponse> GenerateVideoUploadToken(int organizationId, int userId, string fileExtension);
        string GenerateProfileImageBlobName(int userId, string fileExtension);
        string GenerateVideoBlobName(int userId, string videoType, string fileExtension);

        // ========== NEW METHODS FOR SAS TOKEN CACHING ==========

        /// <summary>
        /// Create both private and public containers for an organization
        /// </summary>
        Task<(bool Success, string PrivateContainer, string PublicContainer)>
            CreateOrganizationContainersAsync(
                int organizationId,
                string privateContainerName,
                string publicContainerName);

        /// <summary>
        /// Get a cached SAS token for accessing a container (24-hour expiry with automatic caching)
        /// On-demand cleanup of expired tokens (no background service required)
        /// </summary>
        Task<string> GetSasTokenAsync(
            string containerName,
            bool isPublic,
            string permissions);

        /// <summary>
        /// Upload a file to blob storage organized by userId
        /// Path structure: {userId}/{folder}/{fileName}
        /// </summary>
        Task<string> UploadFileAsync(
            Stream fileStream,
            string containerName,
            int userId,
            string fileName,
            string folder = null);

        /// <summary>
        /// Delete a file from blob storage
        /// </summary>
        Task<bool> DeleteFileAsync(
            string containerName,
            string blobName);

        /// <summary>
        /// List files in a container with a prefix (e.g., all files for a userId)
        /// </summary>
        Task<List<string>> ListFilesAsync(
            string containerName,
            string prefix);
    }
}

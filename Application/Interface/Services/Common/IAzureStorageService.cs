using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Application.Interface.Services.Common
{
    /// <summary>
    /// Service for managing Azure Blob Storage operations
    /// Handles container creation, SAS token generation, and file operations
    /// </summary>
    public interface IAzureStorageService
    {
        /// <summary>
        /// Create both private and public containers for an organization
        /// </summary>
        /// <param name="organizationId">Organization ID</param>
        /// <param name="privateContainerName">Name for private container</param>
        /// <param name="publicContainerName">Name for public container</param>
        /// <returns>Success status and container names</returns>
        Task<(bool Success, string PrivateContainer, string PublicContainer)>
            CreateOrganizationContainersAsync(
                int organizationId,
                string privateContainerName,
                string publicContainerName);

        /// <summary>
        /// Get a SAS token for accessing a container (with automatic caching and cleanup)
        /// </summary>
        /// <param name="containerName">Container name</param>
        /// <param name="isPublic">Whether this is a public container</param>
        /// <param name="permissions">Permissions string (e.g., "rwdl")</param>
        /// <returns>SAS token string</returns>
        Task<string> GetSasTokenAsync(
            string containerName,
            bool isPublic,
            string permissions);

        /// <summary>
        /// Upload a file to blob storage organized by userId
        /// </summary>
        /// <param name="fileStream">File stream to upload</param>
        /// <param name="containerName">Target container</param>
        /// <param name="userId">User ID for folder organization</param>
        /// <param name="fileName">File name</param>
        /// <param name="folder">Optional subfolder (e.g., "documents", "photos")</param>
        /// <returns>Blob URL</returns>
        Task<string> UploadFileAsync(
            Stream fileStream,
            string containerName,
            int userId,
            string fileName,
            string folder = null);

        /// <summary>
        /// Delete a file from blob storage
        /// </summary>
        /// <param name="containerName">Container name</param>
        /// <param name="blobName">Full blob path (e.g., "userId/folder/file.pdf")</param>
        /// <returns>True if deleted successfully</returns>
        Task<bool> DeleteFileAsync(
            string containerName,
            string blobName);

        /// <summary>
        /// List files in a container with a prefix (e.g., all files for a userId)
        /// </summary>
        /// <param name="containerName">Container name</param>
        /// <param name="prefix">Blob prefix (e.g., "userId/folder/")</param>
        /// <returns>List of blob names</returns>
        Task<List<string>> ListFilesAsync(
            string containerName,
            string prefix);
    }
}

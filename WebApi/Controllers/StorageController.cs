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
        public async Task<IActionResult> Post(StorageTokenRequest request)
        {
            var role = _currentUserService.Role;
            var orgId = request.OrganizationId;
            if (role != Domain.Entities.Enum.Role.SUPERADMIN) orgId = _currentUserService.OrganizationId;
            var organization = await _organizationService.GetOrganizationAsync(orgId);
            if (organization is null) return NotFound("Organization not found");
            string containerName = request.IsPrivate ? organization.StorageContainerName : $"{organization.StorageContainerName}-public";
            var serviceResult = _azureStorageService.GenerateToken(containerName, role);
            if (!serviceResult.IsSuccess) ServiceResponseToActionResult.Error(serviceResult.ErrorMessage, serviceResult.ErrorStatus);
            return Ok(serviceResult.Data);
        }

        public class StorageTokenRequest
        {
            public int OrganizationId { get; set; }
            public bool IsPrivate { get; set; }
        }
    }
}

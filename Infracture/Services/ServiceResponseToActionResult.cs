using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Services
{
    public static class ServiceResponseToActionResult
    {
        public static IActionResult Error(string errorMessage, ServiceErrorStatus status)
        {
            return status switch
            {
                ServiceErrorStatus.UNAUTHORIZED => new ObjectResult(errorMessage) { StatusCode = 401},
                ServiceErrorStatus.FORBIDDEN => new ObjectResult(errorMessage) { StatusCode = 403},
                ServiceErrorStatus.NOTFOUND => new ObjectResult(errorMessage) { StatusCode = 404 },
                ServiceErrorStatus.INVALIDOPERATION => new ObjectResult(errorMessage) { StatusCode = 400 },
                _ => new ObjectResult(errorMessage) { StatusCode = 400 }
            };
        }
        public static IActionResult Error(ServiceErrorStatus status)
        {
            return status switch
            {
                ServiceErrorStatus.UNAUTHORIZED => new UnauthorizedResult(),
                ServiceErrorStatus.FORBIDDEN => new ForbidResult(),
                ServiceErrorStatus.NOTFOUND => new NotFoundResult(),
                ServiceErrorStatus.INVALIDOPERATION => new BadRequestResult(),
                _ => new BadRequestResult()
            };
        }
    }
}

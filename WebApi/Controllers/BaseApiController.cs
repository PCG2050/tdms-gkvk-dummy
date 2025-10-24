namespace WebApi.Controllers
{
    [ApiController]
    public abstract class BaseApiController : ControllerBase
    {
        protected IActionResult HandleServiceResult<T>(ServiceResult<T> result)
        {
            var statusCode = GetStatusCode(result.ErrorStatus);

            var response = new
            {
                isSuccess = result.IsSuccess,
                message = result.IsSuccess
                    ? result.SuccessMessage ?? "Success!"
                    : result.ErrorMessage ?? "An unexpected error occurred.",
                statusCode,
                data = result.Data
            };

            return StatusCode(statusCode, response);
        }

        protected IActionResult HandleServiceResult(ServiceResult result)
        {
            var statusCode = GetStatusCode(result.ErrorStatus);

            var response = new
            {
                isSuccess = result.IsSuccess,
                message = result.IsSuccess
                    ? result.SuccessMessage ?? "Success!"
                    : result.ErrorMessage ?? "An unexpected error occurred.",
                statusCode
            };

            return StatusCode(statusCode, response);
        }

        protected int GetStatusCode(ServiceErrorStatus? errorStatus)
        {
            return errorStatus switch
            {
                ServiceErrorStatus.NOTFOUND => StatusCodes.Status404NotFound,
                ServiceErrorStatus.UNAUTHORIZED => StatusCodes.Status401Unauthorized,
                ServiceErrorStatus.FORBIDDEN => StatusCodes.Status403Forbidden,
                ServiceErrorStatus.BADREQUEST => StatusCodes.Status400BadRequest,
                ServiceErrorStatus.INVALIDOPERATION => StatusCodes.Status400BadRequest,
                ServiceErrorStatus.CONFLICT => StatusCodes.Status409Conflict,
                _ => StatusCodes.Status200OK
            };
        }
    }
}

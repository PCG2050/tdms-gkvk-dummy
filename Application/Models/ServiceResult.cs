using System;

namespace Application.Models
{
    public class ServiceResult<T> : IServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string? SuccessMessage { get; private set; }
        public string? ErrorMessage { get; private set; }
        public T? Data { get; private set; }
        public ServiceErrorStatus ErrorStatus { get; private set; } = ServiceErrorStatus.NONE;

        public static ServiceResult<T> Success(T data, string? message = null) => new()
        {
            IsSuccess = true,
            Data = data,
            SuccessMessage = message ?? "Operation successful."
        };

        public static ServiceResult<T> Failure(string message, ServiceErrorStatus status = ServiceErrorStatus.INVALIDOPERATION) => new()
        {
            IsSuccess = false,
            ErrorMessage = message,
            ErrorStatus = status
        };
    }

    public class ServiceResult : IServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string? SuccessMessage { get; private set; }
        public string? ErrorMessage { get; private set; }
        public ServiceErrorStatus ErrorStatus { get; private set; } = ServiceErrorStatus.NONE;

        public static ServiceResult Success(string? message = null) => new()
        {
            IsSuccess = true,
            SuccessMessage = message ?? "Operation successful."
        };

        public static ServiceResult Failure(string message, ServiceErrorStatus status = ServiceErrorStatus.INVALIDOPERATION) => new()
        {
            IsSuccess = false,
            ErrorMessage = message,
            ErrorStatus = status
        };
    }

    public interface IServiceResult
    {
        bool IsSuccess { get; }
        string? ErrorMessage { get; }
        ServiceErrorStatus ErrorStatus { get; }
    }

    public enum ServiceErrorStatus
    {
        NONE,
        NOTFOUND,
        UNAUTHORIZED,
        FORBIDDEN,
        INVALIDOPERATION,
        CONFLICT,
        BADREQUEST,
        VALIDATIONERROR
    }
}

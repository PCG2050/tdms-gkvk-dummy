using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class ServiceResult<T>: IServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; } = null!;
        public T Data { get; private set; }
        public ServiceErrorStatus ErrorStatus { get; private set; } = ServiceErrorStatus.NONE;

        public static ServiceResult<T> Failure(string message, ServiceErrorStatus status = ServiceErrorStatus.INVALIDOPERATION) => new()
        {
            IsSuccess = false,
            ErrorMessage = message,
            ErrorStatus = status
        };
        public static ServiceResult<T> Success(T data) => new()
        {
            IsSuccess = true,
            Data = data
        };

    }
    public class ServiceResult:IServiceResult
    {
        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; } = null!;

        public ServiceErrorStatus ErrorStatus { get; private set; } = ServiceErrorStatus.NONE;

        public static ServiceResult Failure(string message, ServiceErrorStatus status = ServiceErrorStatus.INVALIDOPERATION) => new()
        {
            IsSuccess = false,
            ErrorMessage = message,
            ErrorStatus = status
        };
        public static ServiceResult Success() => new()
        {
            IsSuccess = true
        };
    }

    public interface IServiceResult
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }
        public ServiceErrorStatus ErrorStatus { get; }
    }

    public enum ServiceErrorStatus
    {
        NONE,
        NOTFOUND,
        UNAUTHORIZED,
        FORBIDDEN,
        INVALIDOPERATION
    }

}

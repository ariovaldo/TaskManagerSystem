using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TaskManagerSystem.Domain.Base
{
    public class ApiResult<T>
    {
        public int StatusCode { get; private set; } = StatusCodes.Status200OK;
        public T? Data { get; private set; }
        public static ApiResult<T> CreateInstance() => new ApiResult<T>();

        public ICollection<ApiError> Errors { get; private set; } = new List<ApiError>();

        [JsonIgnore]
        public bool HasError => Errors.Any();

        public ApiResult() { }

        public ApiResult(T data)
        {
            Data = data;
        }
        public ApiResult<T> SetData(T data)
        {
            Data = data;
            return this;
        }

        public ApiResult<T> AddServerError(string title = "", string message = "")
        {
            AddError(
                string.IsNullOrEmpty(title) ? "error" : title,
                string.IsNullOrEmpty(message) ? "An error has ocurred. Please try again later." : message
            );
            StatusCode = StatusCodes.Status500InternalServerError;
            return this;
        }

        public ApiResult<T> AddRequestError(string title, string message)
        {
            AddError(title, message);
            StatusCode = StatusCodes.Status400BadRequest;
            return this;
        }

        public ApiResult<T> AddError(string title, string message)
        {
            Errors.Add(new ApiError(title, message));
            return this;
        }

        public ApiResult<T> AddErrorAndCode(string title, string message, HttpStatusCode code)
        {
            var result = (ApiResult<T>)AddError(title, message);
            result.SetResultCode((int)code);
            return result;
        }

        public ApiResult<T> SetResultCode(HttpStatusCode code)
        {
            SetResultCode((int)code);
            return this;
        }


        public void AddError(IEnumerable<ApiError> listError)
        {
            foreach (ApiError error in listError)
            {
                if (string.IsNullOrWhiteSpace(error.Title))
                    AddError(error.Message);
                else
                    AddError(error.Title, error.Message);
            }
        }

        private ApiResult<T> AddError(string message)
        {
            return (ApiResult<T>)AddError("error", message);
        }

        private ApiResult<T> SetResultCode(int code)
        {
            StatusCode = code;
            return this;
        }

        

    }
}

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Serilog;
using System.Net;
using System.Text.Json;
using TaskManagerSystem.Domain.Base;

namespace TaskManagerSystem.API.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger;
        }

        public async Task Invoke(HttpContext context, IWebHostEnvironment env)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception,"Error");

                var apiResult = ApiResult<string>.CreateInstance().AddServerError(message: env.IsEnvironment("DEV") ? exception.ToString() : null);
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                await WriteJsonResponse(context, apiResult);
            }
        }

        static async Task WriteJsonResponse(HttpContext context, ApiResult<string> errorResponseModel, string contentType = null)
        {
            context.Response.ContentType = contentType ?? "application/json";

            var jsonResult = JsonConvert.SerializeObject(
                errorResponseModel,
                Formatting.Indented,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

            await context.Response.WriteAsync(jsonResult);
        }
    }
}

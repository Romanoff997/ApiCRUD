using Newtonsoft.Json;
using System.Net;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using ApiCRUD.Models;
using ApiCRUD.Services;

namespace ApiCRUD.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IJsonConverter _converter;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger, JsonNewtonConverter converter)
        {
            _next = next;
            _logger = logger;
            _converter = converter;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                ErrorClientResponseModel errorResponse = new ErrorClientResponseModel();
                switch (error)
                {
                    case ApplicationException e:
                        // custom application error
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        errorResponse.status = (int)HttpStatusCode.BadRequest;
                        errorResponse.code = "VALIDATION_EXCEPTION";
                        foreach (var curr in _converter.ReadJson<Dictionary<string, string>>(error.Message.ToString()))
                        {
                            errorResponse.exception.Add(new ExceptionClientResponse() { field = curr.Key, message = curr.Value });
                        }
                        break;
                    default:
                        // unhandled error
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        errorResponse.status = (int)HttpStatusCode.InternalServerError;
                        errorResponse.code = "INTERNAL_SERVER_ERROR";

                        break;
                }

                var result = _converter.WriteJson(errorResponse);
                await response.WriteAsync(result);
            }
        }
    }
}

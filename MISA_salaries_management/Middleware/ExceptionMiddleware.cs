using Microsoft.AspNetCore.Http;
using MISA_Core.Dto.Response;
using MISA_Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;

            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;
            try
            {
                await _next(context);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var body = await new StreamReader(memoryStream).ReadToEndAsync();

                context.Response.Body = originalBody;

                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                {
                    var apiResponse = ApiResponseHelper.Success(JsonSerializer.Deserialize<object>(body));
                    await context.Response.WriteAsync(JsonSerializer.Serialize(apiResponse));
                }
            }
            catch (ValidateException ex)
            {
                context.Response.Body = originalBody;
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                context.Response.Body = originalBody;
                await HandleExceptionAsync(context, ex, HttpStatusCode.InternalServerError);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex, HttpStatusCode code)
        {

            context.Response.Clear();
            context.Response.StatusCode = (int)code;
            context.Response.ContentType = "application/json; charset=utf-8";

            APIResponse<object> response;

            // Lỗi nghiệp vụ (ValidateException)
            if (ex is ValidateException validateEx)
            {
                response = ApiResponseHelper.Error<object>(
                    userMessage: "Dữ liệu không hợp lệ.",
                    systemMessage: ex.Message
                );

                response.ValidateInfo = validateEx.Errors.Select(e => new
                {
                    Field = e.Key,
                    Message = e.Value
                });
            }
            // Các loại lỗi khác
            else
            {
                response = ApiResponseHelper.Error<object>(
                    userMessage: "Lỗi hệ thống",
                    systemMessage: ex.Message
                );
            }

            var json = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(json);
        }
    }
}

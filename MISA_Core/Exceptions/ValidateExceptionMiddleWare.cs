using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class ValidateExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ValidateExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidateException vex)
            {
                context.Response.StatusCode = 400;
                var res = new
                {
                    message = "Thông tin nhập không hợp lệ",
                    data = vex.Errors
                };
                var resJson = System.Text.Json.JsonSerializer.Serialize(res);
                await context.Response.WriteAsync(resJson);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                var res = new
                {
                    errors = new
                    {
                        message = "Lỗi hệ thống",
                        data = ex.Message
                    }
                };

                var json = System.Text.Json.JsonSerializer.Serialize(res);
                await context.Response.WriteAsync(json);
            }
        }
    }
}

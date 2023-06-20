using CargoApi.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace CargoApi.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(AppException e)
            {
                await WriteError(context, 400, e.Message);
            }
            catch
            {
                await WriteError(context, 500, "Internal Server Error");
            }
        }

        public async Task WriteError(HttpContext context, int statusCode, string message)
        {            
            var obj = new
            {
                Message = message
            };

            string json = JsonConvert.SerializeObject(obj);

            context.Response.Clear();

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            
            await context.Response.WriteAsync(json);
        }
    }
}

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ManagerApi4.Middleware
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate next;

        public LoggerMiddleware(RequestDelegate _next)
        {
            next = _next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                string message = "[Request] HTTP " + context.Request.Method + "-" + context.Request.Path;
                await next(context);

                message = "[Response] HTTP" + context.Request.Method + "-" + context.Request.Path + "responsed" + context.Response.StatusCode;
                Console.WriteLine(message);
            }
            catch (Exception ex)
            {
                await ExceptionHandle(context, ex);

            }
        }

        private  Task ExceptionHandle(HttpContext context, Exception ex)
        {
            string message = "[Error] HTTP" + context.Request.Method + "-" + context.Response.StatusCode + "Error Message:" + ex.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var result = JsonConvert.SerializeObject(new { error = ex.Message }, Formatting.None);
            return context.Response.WriteAsync(result);
        }

    }
    public static class LoggerMiddlewareExtension
    {
        public static IApplicationBuilder UseLoggerExceptionMiddle(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LoggerMiddleware>();
        }  
    }
}

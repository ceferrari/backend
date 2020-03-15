using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Backend.Shared.Constants;
using Backend.Shared.Utilities;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace Backend.Shared.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private const HttpStatusCode Code = HttpStatusCode.InternalServerError;
        private const string Message = ErrorMessages.UnexpectedError;
        private const string ContentType = "application/json";

        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            Log.Error(ex, Message);

            var result = new List<string> { $"{Message}: {ex.Message}" };
            var json = JsonUtilities.Serialize(result);

            context.Response.StatusCode = (int)Code;
            context.Response.ContentType = ContentType;

            return context.Response.WriteAsync(json);
        }

        //public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        //{
        //    app.UseExceptionHandler(options =>
        //    {
        //        options.Run(async context =>
        //        {
        //            var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
        //            if (exceptionHandlerFeature != null)
        //            {
        //                await HandleExceptionAsync(context, exceptionHandlerFeature.Error);
        //            }
        //        });
        //    });
        //}
    }
}

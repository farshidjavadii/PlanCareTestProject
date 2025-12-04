using Application.Exceptions;
using System.Collections.Generic;

namespace WebApi.Middleware
{
    public static class ErrorHandling
    {
        public static void ExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appBuilder =>
            {
                appBuilder.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerPathFeature = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();
                    var exception = exceptionHandlerPathFeature?.Error;

                    int statusCode = 500;
                    string message = "An unexpected error occurred.";

                    if (exception is NotFoundException)
                    {
                        statusCode = 404;
                        message = exception.Message;
                    }
                    else if (exception is ValidationException)
                    {
                        statusCode = 400;
                        message = "Validation failed.";
                    }

                    context.Response.StatusCode = statusCode;

                    var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
                    logger.LogError(exception,
                        "Handling exception for request {Path} with status code {StatusCode}",
                        context.Request.Path, statusCode);


                    string detail = app.Environment.IsDevelopment() ? exception?.StackTrace : null;

                    var errorResponse = new
                    {
                        statusCode = context.Response.StatusCode,
                        message = message,
                        detail = detail
                    };

                    await context.Response.WriteAsJsonAsync(errorResponse);
                });
            });

        }
    }
}

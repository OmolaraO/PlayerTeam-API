using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;
using WebApi.DTOs;
using WebApi.Exceptions;

namespace WebApi.Extentions
{
    public static  class ExceptionMiddlewareExtension
    {
        public static void ConfigureExceptionHandler(this WebApplication app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException => StatusCodes.Status400BadRequest,
                            DuplicateRequestException => StatusCodes.Status409Conflict,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        var errorResponse = new GenericResponse<string>()
                        {
                            Message = contextFeature.Error.Message,
                        };
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
                    }


                });
            }
            );
        }
    }
}


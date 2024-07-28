using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tudo_List.Domain.Exceptions;
using DtoValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Tudo_List.Server.Extensions
{
    public static class ProblemDetailsExtensions
    {
        private static readonly Dictionary<Type, int> StatusCodeByExceptionType = new()
        {
            { typeof(ValidationException), StatusCodes.Status400BadRequest },
            { typeof(EntityNotFoundException), StatusCodes.Status404NotFound },
            { typeof(ArgumentNullException), StatusCodes.Status500InternalServerError },
            { typeof(ArgumentException), StatusCodes.Status500InternalServerError },
            { typeof(BadHttpRequestException), StatusCodes.Status400BadRequest },
            { typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized },
            { typeof(DtoValidationException), StatusCodes.Status400BadRequest },
            { typeof(ArgumentOutOfRangeException), StatusCodes.Status400BadRequest }
        };

        private const string DefaultValidationErrorTitle = "Validation Failed!";
        private const string DefaultValidationErrorDetail = "Please refer to the errors property for additional details";

        public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app, ILoggerFactory loggerFactory)
        {

            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var jsonSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    };

                    if (exceptionHandlerFeature is not null)
                    {
                        var exception = exceptionHandlerFeature.Error;
                        StatusCodeByExceptionType.TryGetValue(exception.GetType(), out int statusCode);

                        if (exception is ValidationException validationException)
                        {
                            var problemDetails = new ValidationProblemDetails
                            {
                                Status = statusCode,
                                Instance = context.Request.HttpContext.Request.Path,
                            };

                            if (validationException.Errors.Any())
                            {
                                problemDetails.Title = DefaultValidationErrorTitle;
                                problemDetails.Detail = DefaultValidationErrorDetail;
                                problemDetails.Errors = validationException.Errors.ToDictionary(x => x.PropertyName, x => new string[] { x.ErrorMessage }); ;
                            }
                            else
                            {
                                problemDetails.Title = validationException.Message;
                                problemDetails.Detail = validationException.StackTrace;
                            }

                            context.Response.StatusCode = problemDetails.Status.Value;
                            context.Response.ContentType = "application/problem+json";

                            var json = JsonConvert.SerializeObject(problemDetails, jsonSettings);
                            await context.Response.WriteAsync(json);
                        }
                        else
                        {
                            var problemDetails = new ProblemDetails
                            {
                                Title = exception.Message,
                                Status = statusCode,
                                Instance = context.Request.HttpContext.Request.Path,
                                Detail = exception.StackTrace
                            };

                            context.Response.StatusCode = problemDetails.Status.Value;
                            context.Response.ContentType = "application/problem+json";

                            var json = JsonConvert.SerializeObject(problemDetails, jsonSettings);
                            await context.Response.WriteAsync(json);
                        }
                    }
                });
            });
        }

        public static IServiceCollection ConfigureProblemDetailsModelState(this IServiceCollection services)
        {
            return services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Title = DefaultValidationErrorTitle,
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodeByExceptionType[typeof(DtoValidationException)],
                        Detail = DefaultValidationErrorDetail
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });
        }
    }
}

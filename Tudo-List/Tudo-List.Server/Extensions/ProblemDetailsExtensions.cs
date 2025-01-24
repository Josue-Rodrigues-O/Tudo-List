using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tudo_List.Server.Helpers;

namespace Tudo_List.Server.Extensions
{
    public static class ProblemDetailsExtensions
    {
        public static void UseProblemDetailsExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (exceptionHandlerFeature is not null)
                    {
                        var problemDetails = exceptionHandlerFeature.Error is ValidationException validationException
                            ? ProblemDetailsHelper.GetValidationProblemDetails(validationException, context)
                            : ProblemDetailsHelper.GetProblemDetails(exceptionHandlerFeature.Error, context);

                        context.Response.StatusCode = problemDetails.Status!.Value;
                        context.Response.ContentType = "application/problem+json";
                        
                        var json = JsonConvert.SerializeObject(problemDetails, new JsonSerializerSettings 
                        { 
                            ContractResolver = new CamelCasePropertyNamesContractResolver() 
                        });

                        await context.Response.WriteAsync(json);
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
                    return new BadRequestObjectResult(ProblemDetailsHelper.GetValidationProblemDetails(context))
                    {
                        ContentTypes = 
                        { 
                            "application/problem+json", 
                            "application/problem+xml" 
                        }
                    };
                };
            });
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Tudo_List.Domain.Exceptions;
using DtoValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace Tudo_List.Server.Helpers
{
    public static class ProblemDetailsHelper
    {
        private static readonly Dictionary<Type, int> SpecialStatusCodeByExceptionType = new()
        {
            { typeof(ArgumentOutOfRangeException), StatusCodes.Status400BadRequest },
            { typeof(BadHttpRequestException), StatusCodes.Status400BadRequest },
            { typeof(DtoValidationException), StatusCodes.Status400BadRequest },
            { typeof(ValidationException), StatusCodes.Status400BadRequest },
            { typeof(UnauthorizedAccessException), StatusCodes.Status401Unauthorized },
            { typeof(EntityNotFoundException), StatusCodes.Status404NotFound },
        };

        public static ProblemDetails GetProblemDetails(Exception exception, HttpContext context)
        {
            return new ProblemDetails
            {
                Title = exception.Message,
                Status = SpecialStatusCodeByExceptionType.GetValueOrDefault(exception.GetType(), StatusCodes.Status500InternalServerError),
                Instance = context.Request.HttpContext.Request.Path,
                Detail = exception.StackTrace?.TrimStart()
            };
        }

        public static ValidationProblemDetails GetValidationProblemDetails(ValidationException validationException, HttpContext context)
        {
            var validationProblemDetails = new ValidationProblemDetails()
            {
                Title = "Validation Failed!",
                Status = SpecialStatusCodeByExceptionType[typeof(ValidationException)],
                Instance = context.Request.HttpContext.Request.Path,
            };

            if (validationException.Errors.Any())
            {
                validationProblemDetails.Detail = "Please refer to the errors property for additional details";
                validationProblemDetails.Errors = validationException.Errors.ToDictionary(error => error.PropertyName, error => new string[] { error.ErrorMessage });
            }
            else
            {
                const string errorProperty = "error";

                validationProblemDetails.Detail = $"Please refer to the {errorProperty} property for additional details";
                validationProblemDetails.Extensions.Add(errorProperty, validationException.Message);
            }

            return validationProblemDetails;
        }

        public static ValidationProblemDetails GetValidationProblemDetails(ActionContext context)
        {
            return new ValidationProblemDetails(context.ModelState)
            {
                Title = "Validation Failed!",
                Status = SpecialStatusCodeByExceptionType[typeof(DtoValidationException)],
                Detail = "Please refer to the errors property for additional details",
                Instance = context.HttpContext.Request.Path,
            };
        }
    }
}

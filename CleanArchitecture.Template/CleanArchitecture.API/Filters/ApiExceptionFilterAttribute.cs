using CleanArchitecture.Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.API.Filters
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            _logger = logger;

            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(ValidationException), HandleValidationException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(BadRequestException), HandleBadRequestException },
                { typeof(NotImplementedException), HandleNotImplementedException },
                { typeof(ForbiddenException), HandleForbiddenException }
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            var type = context.Exception.GetType();

            if(_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);

                return;
            }
            else
            {
                HandleUnknownException(context);
            }

        }

        private void HandleUnknownException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Unknown exception.");

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An error occurred while processing your request.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.1"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }

        private void HandleValidationException(ExceptionContext context)
        {
            var exception = context.Exception as ValidationException;

            var details = new ValidationProblemDetails(exception?.Failures)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1"
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            var exception = context.Exception as NotFoundException;

            _logger.LogError(exception?.UiMessage);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "The specified resource was not found.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Detail = exception?.UiMessage
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotImplementedException(ExceptionContext context)
        {
            var exception = context.Exception as NotImplementedException;

            _logger.LogError(context.Exception, "Not yet implemented exception.");

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status501NotImplemented,
                Title = "Feature not yet implemented.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.6.2"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status501NotImplemented
            };

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenException(ExceptionContext context)
        {
            var exception = context.Exception as ForbiddenException;

            _logger.LogError(exception?.UiMessage);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "The specified resource is forbidden.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3",
                Detail = exception?.UiMessage
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            var exception = context.Exception as BadRequestException;

            _logger.LogError(exception?.UiMessage);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "The request cannot be processed.",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Detail = exception?.UiMessage
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status400BadRequest
            };

            context.ExceptionHandled = true;
        }
    }
}

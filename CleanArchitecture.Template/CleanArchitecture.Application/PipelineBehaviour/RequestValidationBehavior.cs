namespace CleanArchitecture.Application.PipelineBehaviour
{
    using FluentValidation;
    using MediatR;
    using ValidationException = Exceptions.ValidationException;

    /// <summary>
    ///     Handle validation behavior in the MediatR pre handling pipeline.
    /// </summary>
    /// <typeparam name="TRequest">Usually the command or query model you wish to validate.</typeparam>
    /// <typeparam name="TResponse">Usually the delegate to the next handler in the pipeline.</typeparam>
    public class RequestValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public RequestValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }

            return await next();
        }
    }
}

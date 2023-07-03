namespace Medication.Api.Application.Behaviors
{
    using FluentValidation;
    using MediatR;

    internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest>? _validator;

        public ValidationBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator != null)
            {
                var validationResult = _validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    throw new ValidationException($"Invalid request {typeof(TRequest).Name}.", validationResult.Errors);
                }
            }

            return await next();
        }
    }
}

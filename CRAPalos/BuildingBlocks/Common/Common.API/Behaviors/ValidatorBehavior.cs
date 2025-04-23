using Common.API.Extensions;
using Common.Domain.Exceptions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Common.API.Behaviors;

public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly ILogger<ValidatorBehavior<TRequest, TResponse>> _logger;
    private readonly IValidator<TRequest>[] _validators;

    public ValidatorBehavior(IValidator<TRequest>[] validators, ILogger<ValidatorBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var typeName = request.GetGenericTypeName();

        _logger.LogInformation("----- Validating command {CommandType}", typeName);

        var failures = _validators
            .Select(v => v.ValidateAsync(request, cancellationToken))
            .SelectMany(result => result.Result.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            var errores = failures.Select(x => x.PropertyName + ": " + x.ErrorMessage).AsEnumerable().Aggregate((i, j) => i + ", " + j);

            _logger.LogWarning("Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}", typeName, request, failures);

            throw new DomainException(
                $"Command Validation Errors for type {typeof(TRequest).Name} -> {errores}", new ValidationException("Validation exception", failures));
        }

        return await next();
    }
}

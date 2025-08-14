using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Validations;

public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
	{
        _validators = validators;
        _logger = logger;
	}

	public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
	{
        _logger.LogInformation("Handling {RequestName}", typeof(TRequest).Name);

        if (!_validators.Any())
		{
            var responseNoValidators = await next();
            _logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
            return responseNoValidators;
		}

		var context = new ValidationContext<TRequest>(request);

		var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
		var failures = validationResults.SelectMany(r => r.Errors).Where(f => f is not null).ToList();

        if (failures.Count != 0)
		{
            _logger.LogWarning("Validation failed for {RequestName}: {Errors}", typeof(TRequest).Name, failures.Select(f => f.ErrorMessage));
            throw new ValidationException(failures);
		}
        
        var response = await next();
        _logger.LogInformation("Handled {RequestName}", typeof(TRequest).Name);
        return response;
	}
}



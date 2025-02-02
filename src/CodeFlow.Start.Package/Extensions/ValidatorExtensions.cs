﻿using CodeFlow.Start.Package.WebTransfer.Base;
using CodeFlow.Start.Package.WebTransfer.Base.Response;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CodeFlow.Start.Package.Extensions;

public static class ValidatorExtensions
{
    /// <summary>
    /// Validates an request using FluentValidation and adds error messages to the response if validation fails.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object to validate.</typeparam>
    /// <param name="validator">The FluentValidation validator instance.</param>
    /// <param name="request">The request object to validate.</param>
    /// <param name="response">The response object to add error messages to.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A boolean indicating whether the validation succeeded or not.</returns>
    public static async Task<bool> ValidateAndAddErrorsAsync<TRequest>(this IValidator<TRequest> validator, TRequest request, ILogger logger, BaseResponse response, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            logger.LogWarning("Validation failed. Errors: {@Errors}", validationResult.Errors);
            response.AddErrorMessages<BaseResponse>(ErrorType.BusinessRuleError, validationResult.Errors.Select(e => e.ErrorMessage).ToArray());
            return false;
        }

        return true;
    }
}

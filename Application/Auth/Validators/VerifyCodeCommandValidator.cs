using System;
using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class VerifyCodeCommandValidator : IValidator<VerifyCodeCommand>
{
    public ValidationResult Validate(VerifyCodeCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(instance.Code))
            validationResult.AddError("Code is required.");
        
        if (instance.Code.Length is > 6 or < 6)
            validationResult.AddError("Code must be 6 characters long.");

        return validationResult;
    }
}

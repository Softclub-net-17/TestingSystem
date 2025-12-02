using System;
using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class RequestChangeEmailValidator : IValidator<RequestChangeEmailCommand>
{
    public ValidationResult Validate(RequestChangeEmailCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if(string.IsNullOrWhiteSpace(instance.NewEmail))
            validationResult.AddError("Email is required.");
        
        if (!instance.NewEmail.Contains('@'))
            validationResult.AddError("Email is not in correct format.");

        return validationResult;
    }
}

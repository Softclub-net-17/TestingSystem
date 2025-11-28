using System;
using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class RequestResetPasswordValidator : IValidator<RequestResetPasswordCommand>
{
    public ValidationResult Validate(RequestResetPasswordCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if(string.IsNullOrWhiteSpace(instance.Email))
            validationResult.AddError("Email is required.");
        
        if (!instance.Email.Contains('@'))
            validationResult.AddError("Email is not in correct format.");

        return validationResult;
    }
}

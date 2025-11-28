using System;
using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class ResetPasswordCommandValidator : IValidator<ResetPasswordCommand>
{
    public ValidationResult Validate(ResetPasswordCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(instance.Email))
            validationResult.AddError("Email is required.");
        
        if (!instance.Email.Contains('@'))
            validationResult.AddError("Email is not in correct format.");
        
        if (string.IsNullOrWhiteSpace(instance.NewPassword))
            validationResult.AddError("New password is required.");
        
        if (instance.NewPassword != instance.ConfirmPassword)
            validationResult.AddError("New password and confirm password do not match.");

        return validationResult;
    }
}

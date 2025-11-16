using System;
using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class RegisterValidator : IValidator<RegisterCommand>
{
    public ValidationResult Validate(RegisterCommand instance)
    {
        var validationResult = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.FullName))
            validationResult.AddError(" FullName is required.");
        if (instance.FullName.Trim().Length < 3)
            validationResult.AddError("Too short name");

        if (instance.FullName.Trim().Length > 200)
            validationResult.AddError("Too long name");
        
        if (string.IsNullOrWhiteSpace(instance.Email))
            validationResult.AddError("Email is required.");

        if (!instance.Email.Contains('@'))
            validationResult.AddError("Email is not in correct format.");

        if (instance.Email.Trim().Length < 20)
            validationResult.AddError("Too short email");

        if (instance.Email.Trim().Length > 200)
            validationResult.AddError("Too long email");

        if (string.IsNullOrWhiteSpace(instance.Password))
            validationResult.AddError("Password is required.");

        if (instance.Password.Trim().Length <8)
            validationResult.AddError("Too short password");
        
        if (instance.Password != instance.ConfirmPassword)
            validationResult.AddError("Password and confirm password do not match.");
        
        return validationResult;
    }
}

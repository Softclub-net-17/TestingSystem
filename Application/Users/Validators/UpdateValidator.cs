using System;
using Application.Common.Validations;
using Application.Interfaces;
using Application.Users.Commands;

namespace Application.Users.Validators;

public class UpdateValidator: IValidator<UpdateUserCommand>
{
    public ValidationResult Validate(UpdateUserCommand instance)
    {
        var validationResult = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.FullName))
            validationResult.AddError(" FullName is required.");
        if (instance.FullName.Trim().Length < 3)
            validationResult.AddError("Too short name");

        if (instance.FullName.Trim().Length > 200)
            validationResult.AddError("Too long name");
    
        return validationResult;
    }
}
using System;
using Application.Common.Validations;
using Application.Interfaces;
using Application.TestSessions.Commands;

namespace Application.TestSessions.Validators;

public class UpdateTestSessionValidator : IValidator<UpdateTestSessionCommand>
{
    public ValidationResult Validate(UpdateTestSessionCommand instance)
    {
        var validationResult= new ValidationResult();

        if(instance.CompletedAt== DateTime.MinValue)   
            validationResult.AddError("Incorrect Completed time");
        return validationResult;
    }
}

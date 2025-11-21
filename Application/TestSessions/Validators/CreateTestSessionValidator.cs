using System;
using Application.Common.Validations;
using Application.Interfaces;
using Application.TestSessions.Commands;

namespace Application.TestSessions.Validators;

public class CreateTestSessionValidator : IValidator<CreateTestSessionCommand>
{
    public ValidationResult Validate(CreateTestSessionCommand instance)
    {
        var validationResult= new ValidationResult();

        if(instance.SectionId<=0)
            validationResult.AddError("Incorrect Section ID");
        if(instance.UserId<=0)
            validationResult.AddError("Incorrect User ID");
        if(instance.StartedAt== DateTime.MinValue || instance.StartedAt>DateTime.Now)   
            validationResult.AddError("Incorrect Start time");
        return validationResult;
    }
}

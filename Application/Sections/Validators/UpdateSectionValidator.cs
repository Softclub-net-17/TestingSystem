using System;
using Application.Common.Validations;
using Application.Interfaces;
using Application.Sections.Commands;

namespace Application.Sections.Validators;

public class UpdateSectionValidator : IValidator<UpdateSectionCommand>
{
    public ValidationResult Validate(UpdateSectionCommand instance)
    {
        var validationResult= new ValidationResult();

        if(string.IsNullOrWhiteSpace(instance.Name))
            validationResult.AddError("Section name is required");
        if(instance.Name.Trim().Length<3)
            validationResult.AddError("Section name must contain at least 3 characters");
        if(instance.Name.Trim().Length>150)
            validationResult.AddError("Section name is too long");
        return validationResult;
    
    }
}

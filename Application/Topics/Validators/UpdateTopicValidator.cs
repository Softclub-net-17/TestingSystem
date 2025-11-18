using System;
using Application.Common.Validations;
using Application.Interfaces;
using Application.Topics.Commands;

namespace Application.Topics.Validators;

public class UpdateTopicValidator : IValidator<UpdateTopicCommand>
{
    public ValidationResult Validate(UpdateTopicCommand instance)
    {
        var validationResult= new ValidationResult();
        if(string.IsNullOrWhiteSpace(instance.Title))
            validationResult.AddError("Title is required");
        if(instance.Title.Trim().Length<3)
            validationResult.AddError("Title must have at least 3 characters");
        if(instance.Title.Trim().Length>200)
            validationResult.AddError("Title is too long (max-200characters)");
        return validationResult;
    }
}

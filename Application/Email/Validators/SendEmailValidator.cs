using System;
using Application.Common.Validations;
using Application.Email.Commands;
using Application.Interfaces;

namespace Application.Email.Validators;

public class SendEmailValidator : IValidator<SendEmailCommand>
{
    public ValidationResult Validate(SendEmailCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if (instance.ReceiverIds.Count == 0)
            validationResult.AddError("At least one receiver ID is required.");
        
        if (string.IsNullOrWhiteSpace(instance.Subject))
            validationResult.AddError("Subject is required.");
        
        if(instance.Subject.Length > 200)
            validationResult.AddError("Subject cannot exceed 200 characters.");
        
        if (string.IsNullOrWhiteSpace(instance.Body))
            validationResult.AddError("Body is required.");
        
        return validationResult;
    }
}

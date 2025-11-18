using Application.Common.Validations;
using Application.Interfaces;
using Application.Questions.Commands;

namespace Application.Questions.Validators;

public class CreateQuestionValidator : IValidator<CreateQuestionCommand>
{
    public ValidationResult Validate(CreateQuestionCommand instance)
    {
        var validationResult = new ValidationResult();
        
        if(instance.TopicId <= 0)
            validationResult.AddError("Topic Id cannot be less or equal to zero");
        
        if(string.IsNullOrWhiteSpace(instance.Text))
            validationResult.AddError("Text cannot be empty");
        
        return validationResult;
    }
}
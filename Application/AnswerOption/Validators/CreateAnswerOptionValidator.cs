using Application.AnswerOption.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.AnswerOption.Validators;

public class CreateAnswerOptionValidator : IValidator<CreateAnswerOptionCommand>
{
    public ValidationResult Validate(CreateAnswerOptionCommand instance)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Text))
            result.AddError("Answer option text is required");

        if(instance.QuestionId <= 0)
            result.AddError("Question Id must be greater than 0");
        
        return result;    
    }
}
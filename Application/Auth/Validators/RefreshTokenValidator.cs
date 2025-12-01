using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class RefreshTokenValidator : IValidator<RefreshTokenCommand>
{
    public ValidationResult Validate(RefreshTokenCommand instance)
    {
        var result = new ValidationResult();

        if (string.IsNullOrWhiteSpace(instance.Token))
            result.AddError("Refresh token is required.");

        if (instance.Token.Length < 20)
            result.AddError("Refresh token is invalid.");

        return result;
    }
}
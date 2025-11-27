using Application.Auth.Commands;
using Application.Common.Validations;
using Application.Interfaces;

namespace Application.Auth.Validators;

public class ChangePasswordValidator : IValidator<ChangePasswordCommand>
{
    public ValidationResult Validate(ChangePasswordCommand instance)
    {
        var result = new ValidationResult();
        
        if (string.IsNullOrWhiteSpace(instance.CurrentPassword))
            result.AddError("Current password is required.");

        if (string.IsNullOrWhiteSpace(instance.NewPassword))
            result.AddError("New password is required.");

        if (string.IsNullOrWhiteSpace(instance.ConfirmNewPassword))
            result.AddError("Confirmation is required.");

        if (instance.NewPassword != instance.ConfirmNewPassword)
            result.AddError("Passwords do not match.");

        if (instance.NewPassword == instance.CurrentPassword)
            result.AddError("New password must be different from current.");

        if (instance.NewPassword.Length < 8)
            result.AddError("Password must be at least 8 characters.");

        if (!instance.NewPassword.Any(char.IsDigit))
            result.AddError("Password must contain at least one digit.");

        if (!instance.NewPassword.Any(char.IsUpper))
            result.AddError("Password must contain at least one uppercase letter.");

        if (string.IsNullOrWhiteSpace(instance.NewPassword))
            result.AddError("Password must not contain spaces.");

        return result;
    }
}
using Application.Auth.Commands;
using Application.Common.Results;
using Application.Common.Security;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class ChangePasswordCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<ChangePasswordCommand> validator)
        : ICommandHandler<ChangePasswordCommand, Result<string>>    
{
    public async Task<Result<string>> HandleAsync(ChangePasswordCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(";", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var user = await userRepository.GetByIdItemAsync(command.UserId);
        if (user is null)
            return Result<string>.Fail("User not found.", ErrorType.NotFound);
        
        if (!PasswordHasher.Verify(command.CurrentPassword, user.PasswordHash))
            return Result<string>.Fail("Current password is incorrect.", ErrorType.Unauthorized);
        
        user.PasswordHash = PasswordHasher.HashPassword(command.NewPassword);

        await userRepository.UpdateAsync(user);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "Password changed successfully.");
    }
}
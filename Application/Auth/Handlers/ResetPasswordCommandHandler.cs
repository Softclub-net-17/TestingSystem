using System;
using Application.Auth.Commands;
using Application.Auth.Mappers;
using Application.Common.Results;
using Application.Common.Security;
using Application.Interfaces;
using Application.VarificationCodes.Mappers;
using Domain.Interfaces;

namespace Application.Auth.Handlers;


public class ResetPasswordCommandHandler(
    IUserRepository userRepository,
    IVerificationCodeRepository verificationCodeRepository,
    IValidator<ResetPasswordCommand> validator,
    IUnitOfWork unitOfWork) 
    : ICommandHandler<ResetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ResetPasswordCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var user = await userRepository.FindByEmailAsync(command.Email);
        if (user == null)
        {
            return Result<string>.Fail("User not found.", ErrorType.NotFound);
        }
        
        var passwordHash = PasswordHasher.HashPassword(command.NewPassword);
        user.MapFrom(passwordHash);
        
        var lastCode = await verificationCodeRepository.GetByEmailAsync(user.Email);
        lastCode.DeActivate();

        await unitOfWork.SaveChangesAsync();
        
        return Result<string>.Ok(null, "Password has been reset successfully.");
    }
}

using System;
using Application.Auth.Commands;
using Application.Common.Results;
using Application.Common.Validations;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.Auth.Handlers;

public class ChangeEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IValidator<ChangeEmailCommand> validator,
    IVerificationCodeRepository verificationCodeRepository
    ) : ICommandHandler<ChangeEmailCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ChangeEmailCommand command)
    {
        var validationResult= validator.Validate(command);
        if (!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(s => s)), ErrorType.Validation);
        
        var verificationCode = await verificationCodeRepository.GetByEmailAsync(command.Email);
        if (verificationCode.Expiration < DateTime.UtcNow)
            return Result<string>.Fail("Verification code has expired.", ErrorType.Validation);

        if (verificationCode.IsUsed)
            return Result<string>.Fail("Verification code has already been used.", ErrorType.Validation);

        var userExist= await userRepository.GetByIdItemAsync(command.UserId);
        if(userExist==null)
            return Result<string>.Fail("User not found",ErrorType.NotFound);
        
        userExist.Email=command.Email;
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null,"Email changed successfully!");
    }
}

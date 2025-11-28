using System;
using Application.Auth.Commands;
using Application.Common.Results;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class VerifyCodeCommandHandler(
    IVerificationCodeRepository verificationCodeRepository,
    IValidator<VerifyCodeCommand> validator) 
    : ICommandHandler<VerifyCodeCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(VerifyCodeCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var verificationCode = await verificationCodeRepository.GetByEmailAsync(command.Email);
        if (verificationCode.Expiration < DateTime.UtcNow)
        {
            return Result<string>.Fail("Verification code has expired.", ErrorType.Validation);
        }

        if (verificationCode.IsUsed)
        {
            return Result<string>.Fail("Verification code has already been used.", ErrorType.Validation);
        }
        
        return Result<string>.Ok(null, "Verification code is valid.");
    }
}

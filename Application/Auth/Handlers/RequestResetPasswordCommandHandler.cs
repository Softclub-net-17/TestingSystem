using System;
using Application.Auth.Commands;
using Application.Common.Helpers;
using Application.Common.Results;
using Application.Interfaces;
using Application.VarificationCodes.Mappers;
using Domain.Interfaces;

namespace Application.Auth.Handlers;


public class RequestResetPasswordCommandHandler(
    IUserRepository userRepository,
    IVerificationCodeRepository verificationCodeRepository,
    IUnitOfWork unitOfWork,
    IValidator<RequestResetPasswordCommand> validator,
    IEmailService emailService)
    : ICommandHandler<RequestResetPasswordCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(RequestResetPasswordCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(s => s));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var exists = await userRepository.ExistsByEmailAsync(command.Email);
        if (!exists)
        {
            return Result<string>.Fail("Email not found.", ErrorType.NotFound);
        }

        var code = VerificationCodeGenerator.GenerateCode();

        var verificationCode = VerificationCodeMappers.ToEntity(code, command.Email);
        await verificationCodeRepository.CreateAsync(verificationCode);
        await unitOfWork.SaveChangesAsync();

        await emailService.SendEmailAsync([command.Email], "Password Reset Verification Code",
            $"Your password reset verification code is: {code}. It will expire in 2 minutes.");

        return Result<string>.Ok(null, "Verification code sent to email.");
    }
}

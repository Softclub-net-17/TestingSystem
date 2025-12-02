using System;
using Application.Auth.Commands;
using Application.Common.Helpers;
using Application.Common.Results;
using Application.Interfaces;
using Application.VarificationCodes.Mappers;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class RequestChangeEmailCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IVerificationCodeRepository verificationCodeRepository,
    IValidator<RequestChangeEmailCommand> validator
) : ICommandHandler<RequestChangeEmailCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(RequestChangeEmailCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(s => s)), ErrorType.Validation);
        var userExists = await userRepository.GetByIdItemAsync(command.UserId);
        if (userExists==null)
            return Result<string>.Fail("User not found.", ErrorType.NotFound);
        var noChange= userExists.Email.Equals(command.NewEmail, StringComparison.CurrentCultureIgnoreCase);
        if(noChange)
            return Result<string>.Fail("New email is the same as current",ErrorType.NoChange);
        var emailExists= await userRepository.ExistsByEmailAsync(command.NewEmail);
        if(emailExists)
            return Result<string>.Fail("User with this email already exists!",ErrorType.Conflict);
        var code = VerificationCodeGenerator.GenerateCode();

        var verificationCode = VerificationCodeMappers.ToEntity(code, command.NewEmail);
        await verificationCodeRepository.CreateAsync(verificationCode);
        await unitOfWork.SaveChangesAsync();

        await emailService.SendEmailAsync([command.NewEmail], "Password Reset Verification Code",
            $"Your password reset verification code is: {code}. It will expire in 2 minutes.");

        return Result<string>.Ok(null, "Verification code sent to email.");
    }
}

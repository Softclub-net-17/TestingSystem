using System;
using Application.Auth.Commands;
using Application.Auth.Mappers;
using Application.Common.Results;
using Application.Common.Security;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class RegisterCommandHandler(IUserRepository userRepository,
IUnitOfWork unitOfWork,
IValidator<RegisterCommand> validator
) : ICommandHandler<RegisterCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(RegisterCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join(";", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        if (await userRepository.ExistsByEmailAsync(command.Email))
            return Result<string>.Fail("Email is already registeres", ErrorType.Conflict);
        var passwordHash = PasswordHasher.HashPassword(command.Password);

        var user = command.ToEntity(passwordHash);
        
        await userRepository.CreateAsync(user);
        await unitOfWork.SaveChangesAsync();

        return Result<string>.Ok(null, "User registered successfully.");

    }
}

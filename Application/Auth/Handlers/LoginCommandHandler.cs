using System;
using Application.Auth.Commands;
using Application.Common.Results;
using Application.Common.Security;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Auth.Handlers;

public class LoginCommandHandler(IUserRepository userRepository,
IValidator<LoginCommand> validator,
IJwtTokenGenerator jwtTokenGenerator
) : ICommandHandler<LoginCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(LoginCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors.Select(e => e));
            return Result<string>.Fail(errors, ErrorType.Validation);
        }
        
        var user = await userRepository.FindByEmailAsync(command.Email);
        if (user == null)
        {
            return Result<string>.Fail("Invalid email or password.", ErrorType.Unauthorized);
        }
        
        var verify = PasswordHasher.Verify(command.Password, user.PasswordHash);

        if (!verify)
        {
            return Result<string>.Fail("Invalid email or password.", ErrorType.Unauthorized);
        }
        
        var token = jwtTokenGenerator.GenerateToken(user);
        
        return Result<string>.Ok(token, "Login successful.");
    }
}

using Application.Common.Results;
using Application.Email.Commands;
using Application.Interfaces;
using Domain.Interfaces;

namespace Application.Email.Handlers;


public class SendEmailCommandHandler(
    IUserRepository userRepository,
    IEmailService emailService, 
    IValidator<SendEmailCommand> validator) 
    : ICommandHandler<SendEmailCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(SendEmailCommand command)
    {
        var validationResult = validator.Validate(command);
        if (!validationResult.IsValid)
        {
            var errors = string.Join("; ", validationResult.Errors);
            return Result<string>.Fail(errors, ErrorType.Validation);
        }

        var selectedUsers = await userRepository.GetUsersByIdsAsync(command.ReceiverIds);
        
        var emailAddresses = selectedUsers
            .Where(user => !string.IsNullOrWhiteSpace(user.Email))
            .Select(user => user.Email)
            .ToList();
        
        if (emailAddresses.Count == 0)
        {
            return Result<string>.Fail("No valid email addresses found for the provided user IDs.", ErrorType.NotFound);
        }
        
        await emailService.SendEmailAsync(emailAddresses, command.Subject, command.Body);
        
        return Result<string>.Ok(null, "Emails sent successfully.");
    }
}

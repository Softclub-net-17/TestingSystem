using System;
using System.Globalization;
using System.Windows.Input;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.Mappers;
using Domain.Interfaces;

namespace Application.Users.Handlers;

public class UpdateUserCommandHandler(IUserRepository userRepository,IUnitOfWork unitOfWork,IValidator<UpdateUserCommand> validator) : ICommandHandler<UpdateUserCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UpdateUserCommand command)
    {
        var validationResult= validator.Validate(command);
        if (!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
    
        var exist= await userRepository.GetByIdItemAsync(command.Id);
        if(exist==null)
            return Result<string>.Fail("User to update not found",ErrorType.NotFound);
        command.MapFrom(exist);

        await userRepository.UpdateAsync(exist);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"Updated successfully!");

    }
}

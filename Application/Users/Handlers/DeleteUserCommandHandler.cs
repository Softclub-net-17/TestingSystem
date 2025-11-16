using System;
using System.Runtime.Serialization;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.Mappers;
using Domain.Interfaces;

namespace Application.Users.Handlers;

public class DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteUserCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(DeleteUserCommand command)
    {
        var exist= await userRepository.GetByIdItemAsync(command.Id);
        if(exist==null)
            return Result<string>.Fail("User to delete not found",ErrorType.NotFound);
        
        exist.InActive();
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"Deleted successfully!");
        
        
    }
}

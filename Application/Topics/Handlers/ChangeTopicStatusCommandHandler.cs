using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.Commands;
using Application.Topics.Mappers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Logging;

namespace Application.Topics.Handlers;

public class ChangeTopicStatusCommandHandler(
    ISectionRepository sectionRepository,
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<ChangeTopicStatusCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ChangeTopicStatusCommand command)
    {
        var exists=await topicRepository.GetByIdItemAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail($"Topic with given id: {command.Id} not found",ErrorType.NotFound);

        if(command.Status){
        var activeSection= await sectionRepository.GetByIdAsync(exists.SectionId);
        if(!activeSection!.IsActive)
            return Result<string>.Fail("Cannot change topic status to published while section is inactive",ErrorType.Conflict);
        }
        
        exists.ChangeStatus(command.Status);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"Status changed successfully!");
            
    }
}

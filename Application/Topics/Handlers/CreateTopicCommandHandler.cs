using System;
using System.Security.Cryptography;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.Commands;
using Application.Topics.Mappers;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Topics.Handlers;

public class CreateTopicCommandHandler(
    ISectionRepository sectionRepository,
    ITopicRepository topicRepository,
    IValidator<CreateTopicCommand> validator,
    IUnitOfWork unitOfWork
) : ICommandHandler<CreateTopicCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CreateTopicCommand command)
    {
        var validationResult= validator.Validate(command);
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
       
        var sectionExists= await sectionRepository.GetByIdAsync(command.SectionId);
        if(sectionExists==null)
            return Result<string>.Fail($"Section with given id : {command.SectionId} doesnt exist");
        
        var exists= await topicRepository.GetItemByNameAsync(command.Title);
        if(exists!=null)
            return Result<string>.Fail("This topic is already exists",ErrorType.Conflict);
            
        var newTopic=command.ToEntity();
        await topicRepository.CreateItemAsync(newTopic);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null,"Topic created successfully!");
        
    }
}

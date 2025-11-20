using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Topics.Commands;
using Application.Topics.Mappers;
using Domain.Interfaces;

namespace Application.Topics.Handlers;

public class UpdateTopicCommandHandler(
    ISectionRepository sectionRepository,
    ITopicRepository topicRepository,
    IUnitOfWork unitOfWork,
    IValidator<UpdateTopicCommand> validator
) : ICommandHandler<UpdateTopicCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UpdateTopicCommand command)
    {
        var validationResult= validator.Validate(command);
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        var exists=await topicRepository.GetItemByIdAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail("Topic to update doesnt exist");
       
        var sectionExists= await sectionRepository.GetByIdAsync(command.SectionId);
        if(sectionExists==null)
            return Result<string>.Fail($"Section with given id : {command.SectionId} doesnt exist");
        if(!sectionExists.IsActive)
            return Result<string>.Fail("Cannot create topic to inactive section",ErrorType.Conflict);
        var nameExists= await topicRepository.GetItemByNameAsync(command.Title);
        if(nameExists!=null)
            return Result<string>.Fail("This topic is already exists",ErrorType.Conflict);
        var noChange=exists.SectionId==command.SectionId 
        &&exists.Title.Equals(command.Title.Trim(), StringComparison.CurrentCultureIgnoreCase) 
        && exists.Content.Equals(command.Content.Trim(), StringComparison.CurrentCultureIgnoreCase) 
        && exists.IsPublished==command.IsPublished;
        if(noChange)
            return Result<string>.Fail("No changes were made",ErrorType.NoChange);
        command.MapFrom(exists);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null, "Topic updated successfully!");
        

    }
}

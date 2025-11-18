using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Commands;
using Application.Sections.Mappers;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class UpdateSectionCommandHandler(
    IValidator<UpdateSectionCommand> validator, 
    ISectionRepository sectionRepository, 
    IUnitOfWork unitOfWork) 
    : ICommandHandler<UpdateSectionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(UpdateSectionCommand command)
    {
        var validationResult= validator.Validate(command);
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        var exists= await sectionRepository.GetByIdAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail("Section to update not found", ErrorType.NotFound);
        var noChange=exists.Name.ToLower()==command.Name.Trim().ToLower();
        if(noChange)
            return Result<string>.Fail("No changes were made",ErrorType.NoChange);
        
        command.MapFrom(exists);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null, "Section updated successfully");


    }
}

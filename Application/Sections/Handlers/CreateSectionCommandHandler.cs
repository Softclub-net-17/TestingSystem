using System;
using System.Globalization;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Commands;
using Application.Sections.Mappers;
using Domain.Interfaces;

namespace Application.Sections.Handlers;

public class CreateSectionCommandHandler(IValidator<CreateSectionCommand> validator,
 ISectionRepository sectionRepository, 
 IUnitOfWork unitOfWork) 
 : ICommandHandler<CreateSectionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CreateSectionCommand command)
    {
        var validationResult= validator.Validate(command);
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);
        var exist= await sectionRepository.GetItemByNameAsync(command.Name);
        if(exist!=null)
            return Result<string>.Fail($"Section with this name: {command.Name} already exists");
        var newSection= command.ToEntity();
        await sectionRepository.CreateItemsAsync(newSection);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null, "Section created successfully!");
    }
}

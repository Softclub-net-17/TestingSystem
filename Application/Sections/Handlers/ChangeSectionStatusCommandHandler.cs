using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.Sections.Commands;
using Application.Sections.Mappers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Application.Sections.Handlers;

public class ChangeSectionStatusCommandHandler(ISectionRepository sectionRepository, IUnitOfWork unitOfWork) : ICommandHandler<ChangeSectionStatusCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(ChangeSectionStatusCommand command)
    {
        var exists= await sectionRepository.GetByIdAsync(command.Id);
        if(exists==null)
            return Result<string>.Fail($"Section with this id : {command.Id} not found",ErrorType.NotFound);
        exists.ChangeStatus(command.Status);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok(null, "Status changed successfully!");
    }
}


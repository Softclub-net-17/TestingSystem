using System;
using Application.Common.Results;
using Application.Interfaces;
using Application.TestSessions.Commands;
using Application.TestSessions.Mappers;
using Domain.Interfaces;

namespace Application.TestSessions.Handlers;

public class CreateTestSessionCommandHandler(
IValidator<CreateTestSessionCommand> validator,
ITestSessionRepository testSessionRepository,
IUserRepository userRepository,
IUnitOfWork unitOfWork,
ISectionRepository sectionRepository
) : ICommandHandler<CreateTestSessionCommand, Result<string>>
{
    public async Task<Result<string>> HandleAsync(CreateTestSessionCommand command)
    {
        var validationResult= validator.Validate(command);
        if(!validationResult.IsValid)
            return Result<string>.Fail(string.Join("; ", validationResult.Errors.Select(e => e)), ErrorType.Validation);

        var sectionExists=await sectionRepository.GetByIdAsync(command.SectionId);
        if(sectionExists==null)
            return Result<string>.Fail($"Section with given id :{command.SectionId} doesnt exists",ErrorType.NotFound);
        var userExists=await userRepository.GetByIdItemAsync(command.UserId);
        if(userExists==null)
            return Result<string>.Fail($"User with given id :{command.SectionId} doesnt exists",ErrorType.NotFound);
        var newTestSession=command.ToEntity();
        await testSessionRepository.CreateItemAsync(newTestSession);
        await unitOfWork.SaveChangesAsync();
        return Result<string>.Ok("Created successfully!");        
    }
}

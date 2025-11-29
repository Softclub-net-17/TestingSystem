using System;
using Application.AnswerOption.Commands;
using Application.AnswerOption.DTOs;
using Application.AnswerOption.Handlers;
using Application.AnswerOption.Queries;
using Application.AnswerOption.Validators;
using Application.Auth.Commands;
using Application.Auth.DTOs;
using Application.Auth.Handlers;
using Application.Auth.Validators;
using Application.Common.Results;
using Application.Email.Commands;
using Application.Email.Handlers;
using Application.Email.Validators;
using Application.Interfaces;
using Application.Questions.Commands;
using Application.Questions.DTOs;
using Application.Questions.Handlers;
using Application.Questions.Queries;
using Application.Questions.Validators;
using Application.Sections.Commands;
using Application.Sections.DTOs;
using Application.Sections.Handlers;
using Application.Sections.Queries;
using Application.Sections.Validators;
using Application.Statistics.DTOs;
using Application.Statistics.Handlers;
using Application.Statistics.Queries;
using Application.TestSessions.Commands;
using Application.TestSessions.DTOs;
using Application.TestSessions.Handlers;
using Application.TestSessions.Queries;
using Application.TestSessions.Validators;
using Application.Topics.Commands;
using Application.Topics.DTOs;
using Application.Topics.Handlers;
using Application.Topics.Queries;
using Application.Topics.Validators;
using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Handlers;
using Application.Users.Queries;
using Application.Users.Validators;
using Domain.DTOs;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register application services here

        //Auth
        services.AddScoped<ICommandHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();
        services.AddScoped<ICommandHandler<ChangePasswordCommand, Result<string>>, ChangePasswordCommandHandler>();
        services.AddScoped<ICommandHandler<SendEmailCommand, Result<string>>, SendEmailCommandHandler>();
        services.AddScoped<ICommandHandler<RequestResetPasswordCommand, Result<string>>, RequestResetPasswordCommandHandler>();
        services.AddScoped<ICommandHandler<VerifyCodeCommand, Result<string>>, VerifyCodeCommandHandler>();
        services.AddScoped<ICommandHandler<ResetPasswordCommand, Result<string>>, ResetPasswordCommandHandler>();
        services.AddScoped<ICommandHandler<LoginCommand, Result<AuthResponseDto>>, LoginCommandHandler>();
        services.AddScoped<ICommandHandler<RefreshTokenCommand, Result<AuthResponseDto>>, RefreshTokenCommandHandler>();

        
        //User
        services.AddScoped<IQueryHandler<GetUsersQuery, PagedResult<List<GetUserDTO>>>,GetUsersQueryHandler>();
        services.AddScoped<IQueryHandler<GetUserByIdQuery,Result<GetUserDTO>>,GetUserByIdQueryHandler>();
        services.AddScoped<ICommandHandler<UpdateUserCommand, Result<string>>, UpdateUserCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteUserCommand, Result<string>>, DeleteUserCommandHandler>();

        //Questions
        services.AddScoped<IQueryHandler<GetQuestionsQuery, Result<List<GetQuestionDto>>>,GetQuestionsQueryHandler>();
        services.AddScoped<IQueryHandler<GetActiveQuestionsQuery, Result<List<GetActiveQuestionsDto>>>,GetActiveQuestionsQueryHandler>();
        services.AddScoped<IQueryHandler<GetQuestionsByTopicIdQuery, Result<List<GetQuestionDto>>>,GetQuestionsByTopicIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetQuestionByIdQuery,Result<GetQuestionDto>>,GetQuestionByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetTestBySectionIdWithAnswerOptionsQuery, Result<List<GetQuestionWithOptionsDto>>>, GetTestBySectionIdWithAnswerOptionsQueryHandler>();

        services.AddScoped<ICommandHandler<CreateQuestionCommand, Result<string>>, CreateQuestionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateQuestionCommand, Result<string>>, UpdateQuestionCommandHandler>();
        services.AddScoped<ICommandHandler<ChangeQuestionStatusCommand, Result<string>>, ChangeQuestionStatusCommandHandler>();
        
        //AnswerOptions
        services.AddScoped<IQueryHandler<GetAnswerOptionByIdQuery, Result<GetAnswerOptionDto>>, GetAnswerOptionByIdQueryHandler>();
        services.AddScoped<IQueryHandler<GetAnswerOptionByQuestionIdQuery, Result<List<GetAnswerOptionDto>>>, GetAnswerOptionByQuestionIdQueryHandler>();
        services.AddScoped<ICommandHandler<CreateAnswerOptionCommand, Result<string>>, CreateAnswerOptionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateAnswerOptionCommand, Result<string>>, UpdateAnswerOptionCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteAnswerOptionCommand, Result<string>>, DeleteAnswerOptionCommandHandler>();
        
        //Section
        services.AddScoped<IQueryHandler<GetSectionsQuery, PagedResult<List<GetSectionDTO>>>,GetSectionsQueryHandler>();
        services.AddScoped<IQueryHandler<GetActiveSectionsQuery, PagedResult<List<GetSectionDTO>>>,GetActiveSectionsQueryHandler>();
        services.AddScoped<IQueryHandler<GetSectionByIdQuery,Result<GetSectionByIdDto>>,GetSectionByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CreateSectionCommand, Result<string>>, CreateSectionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateSectionCommand, Result<string>>, UpdateSectionCommandHandler>();
        services.AddScoped<ICommandHandler<ChangeSectionStatusCommand, Result<string>>, ChangeSectionStatusCommandHandler>();

        //Topics
        services.AddScoped<IQueryHandler<GetTopicsQuery, PagedResult<List<GetTopicDto>>>,GetTopicsQueryHandler>();
        services.AddScoped<IQueryHandler<GetTopicByIdQuery,Result<GetTopicDto>>,GetTopicByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CreateTopicCommand, Result<string>>, CreateTopicCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateTopicCommand, Result<string>>, UpdateTopicCommandHandler>();
        services.AddScoped<ICommandHandler<ChangeTopicStatusCommand, Result<string>>, ChangeTopicStatusCommandHandler>();
        services.AddScoped<IQueryHandler<GetActiveTopicsQuery, PagedResult<List<GetTopicDto>>>,GetActiveTopicsQueryHandler>();
        services.AddScoped<IQueryHandler<GetTopicsBySectionIdQuery, Result<List<GetTopicBySectionIdDto>>>,GetTopicBySectionIdQueryHandler>();

        //TestSession
        services.AddScoped<IQueryHandler<GetTestSessionsQuery, PagedResult<List<GetTestSessionDto>>>,GetTestSessionsQueryHandler>();
        services.AddScoped<IQueryHandler<GetTestSessionByIdQuery,Result<GetTestSessionDto>>,GetTestSessionByIdQueryHandler>();
        services.AddScoped<ICommandHandler<CreateTestSessionCommand, Result<string>>, CreateTestSessionCommandHandler>();
        services.AddScoped<ICommandHandler<UpdateTestSessionCommand, Result<GetUpdateTestSessionResponseDto>>, UpdateTestSessionCommandHandler>();
        
        //Statistics
        services.AddScoped<IQueryHandler<GetStatisticQuery, Result<GetStatisticDto>>, GetStatisticQueryHandler>();
        services.AddScoped<IQueryHandler<GetAverageSectionStatisticQuery, Result<List<AvarageSectionStatisticDto>>>, GetAverageSectionStatisticQueryHandler>();
        
        //Validators
        //Auth
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();
        services.AddScoped<IValidator<ChangePasswordCommand>, ChangePasswordValidator>();
        services.AddScoped<IValidator<SendEmailCommand>, SendEmailValidator>();
        services.AddScoped<IValidator<RequestResetPasswordCommand>, RequestResetPasswordValidator>();
        services.AddScoped<IValidator<VerifyCodeCommand>, VerifyCodeCommandValidator>();
        services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordCommandValidator>();
        services.AddScoped<IValidator<RefreshTokenCommand>, RefreshTokenValidator>();
        
        //User
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateValidator>();
        
        //Sections
        services.AddScoped<IValidator<CreateSectionCommand>, CreateSectionValidator>();
        services.AddScoped<IValidator<UpdateSectionCommand>,UpdateSectionValidator>();

        //Validators
        services.AddScoped<IValidator<CreateAnswerOptionCommand>, CreateAnswerOptionValidator>();
        services.AddScoped<IValidator<UpdateAnswerOptionCommand>, UpdateAnswerOptionValidator>();
        
        //Questions
        services.AddScoped<IValidator<CreateQuestionCommand>, CreateQuestionValidator>();
        services.AddScoped<IValidator<UpdateQuestionCommand>, UpdateQuestionValidator>();
        
        //Topics
        services.AddScoped<IValidator<CreateTopicCommand>,CreateTopicValidator>();
        services.AddScoped<IValidator<UpdateTopicCommand>, UpdateTopicValidator>();

        //TestSession
        services.AddScoped<IValidator<CreateTestSessionCommand>,CreateTestSessionValidator>();

        

    }

}


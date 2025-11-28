using System;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Persistences.Repositories;
using Infrastructure.Services.Auth;
using Infrastructure.Services.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISectionRepository,SectionRepository>();
        services.AddScoped<ITopicRepository,TopicRepository>();
        services.AddScoped<IQuestionRepository, QuestionRepository>();
        services.AddScoped<IAnswerOptionRepository,AnswerOptionRepository>();
        services.AddScoped<ITestSessionRepository,TestSessionRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IVerificationCodeRepository, VerificationCodeRepository>();
        services.AddScoped<IEmailService, EmailService>();
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        services.Configure<EmailSettings>(configuration.GetSection("SmtpSettings"));

    }
}


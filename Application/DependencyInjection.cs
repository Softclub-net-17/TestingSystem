using System;
using Application.Auth.Commands;
using Application.Auth.Handlers;
using Application.Auth.Validators;
using Application.Common.Results;
using Application.Interfaces;
using Application.Users.Commands;
using Application.Users.DTOs;
using Application.Users.Handlers;
using Application.Users.Queries;
using Application.Users.Validators;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        // Register application services here

        //Auth
        services.AddScoped<ICommandHandler<LoginCommand, Result<string>>, LoginCommandHandler>();
        services.AddScoped<ICommandHandler<RegisterCommand, Result<string>>, RegisterCommandHandler>();

        //User
        services.AddScoped<IQueryHandler<GetUsersQuery, PagedResult<List<GetUserDTO>>>,GetUsersQueryHandler>();
        services.AddScoped<IQueryHandler<GetUserByIdQuery,Result<GetUserDTO>>,GetUserByIdQueryHandler>();
        services.AddScoped<ICommandHandler<UpdateUserCommand, Result<string>>, UpdateUserCommandHandler>();
        services.AddScoped<ICommandHandler<DeleteUserCommand, Result<string>>, DeleteUserCommandHandler>();

        //validators
        //Auth
        services.AddScoped<IValidator<LoginCommand>, LoginValidator>();
        services.AddScoped<IValidator<RegisterCommand>, RegisterValidator>();

        //User
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateValidator>();
        
        

    }

}


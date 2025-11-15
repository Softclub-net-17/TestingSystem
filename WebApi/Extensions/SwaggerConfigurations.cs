using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;


public static class SwaggerConfigurations
{
    public static void AddSwaggerConfigurations(this IServiceCollection services)
    {
        services.AddSwaggerGen(static options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "CRM API",
                Version = "v1",
                Description = "API",
                Contact = new OpenApiContact
                {
                    Name = "Mehrona Asoeva",
                    Email = "asoevamehrona9@gmail.com",
                    Url = new Uri("https://crm.tj")
                }
            });
            
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Введите JWT токен: Bearer {your token}"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
    }
}


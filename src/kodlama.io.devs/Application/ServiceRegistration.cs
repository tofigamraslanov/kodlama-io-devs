using System.Reflection;
using Application.Features.GitHubProfiles.Rules;
using Application.Features.ProgrammingLanguages.Rules;
using Application.Features.Technologies.Rules;
using Application.Features.Users.Rules;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddScoped<ProgrammingLanguageBusinessRules>();
        services.AddScoped<TechnologyBusinessRules>();
        services.AddScoped<GitHubProfileBusinessRules>();
        services.AddScoped<UserBusinessRules>();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));

        return services;
    }
}
using Core.Security.Encryption;
using Core.Security.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebAPI.Extensions;

public static class JwtBearerAuthenticationExtension
{
    public static IServiceCollection AddJwtBearerAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        TokenOptions? tokenOptions = configuration.GetSection("TokenOptions").Get<TokenOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.SaveToken = true;
            options.RequireHttpsMetadata = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = tokenOptions.Issuer,
                ValidAudience = tokenOptions.Audience,
                IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                LifetimeValidator = (_, expires, _, _) => expires != null && expires > DateTime.UtcNow
            };

            options.Events = new JwtBearerEvents()
            {
                OnAuthenticationFailed = _ =>
                {
                    Console.WriteLine("OnAuthenticationFailed:");
                    return Task.CompletedTask;
                },
                OnChallenge = _ =>
                {
                    Console.WriteLine("OnChallenge: ");
                    return Task.CompletedTask;
                },
                OnForbidden = _ =>
                {
                    Console.WriteLine("OnForbidden:");
                    return Task.CompletedTask;
                },
                OnMessageReceived = _ =>
                {
                    Console.WriteLine("OnMessageReceived:");
                    return Task.CompletedTask;
                },
                OnTokenValidated = _ =>
                {
                    Console.WriteLine("OnTokenValidated:");
                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}
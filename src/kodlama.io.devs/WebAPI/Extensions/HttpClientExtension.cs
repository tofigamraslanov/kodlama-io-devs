namespace WebAPI.Extensions;

public static class HttpClientExtension
{
    public static IServiceCollection AddCustomHttpClient(this IServiceCollection services)
    {
        services.AddHttpClient("GitHubUserProfile", config =>
        {
            config.BaseAddress = new Uri("https://api.github.com/users/");
            config.DefaultRequestHeaders.UserAgent.TryParseAdd("request");
        });

        return services;
    }
}
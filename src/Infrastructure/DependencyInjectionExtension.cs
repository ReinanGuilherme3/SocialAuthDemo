using Application.Contracts.Auth;
using Domain.Repositories;
using Domain.Security.ExternalAuth;
using FluentMigrator.Runner;
using Infrastructure.DataAccess;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.Security.ExternalAuth;
using Infrastructure.Security.ExternalAuth.GitHub;
using Infrastructure.Security.ExternalAuth.Google;
using Infrastructure.Security.Token;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System.Reflection;

namespace Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddFluentMigrator(services, configuration);
        AddToken(services, configuration);
        AddExternalAuth(services, configuration);
        AddDbContext(services, configuration);
        AddRepositories(services);
    }


    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var infrastructureAssembly = Assembly.Load("Infrastructure");

        var connectionString = configuration.GetConnectionString("Connection");

        services.AddFluentMigratorCore().ConfigureRunner(config =>
        {
            var migrationRunnerBuilder = config.AddSqlServer();

            migrationRunnerBuilder
            .WithGlobalConnectionString(connectionString)
            .ScanIn(infrastructureAssembly)
            .For.All();
        });
    }

    public static void AddToken(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<TokenSettings>(configuration.GetSection("Token"));

        services.AddSingleton<IToken, Token>();
    }

    public static void AddExternalAuth(this IServiceCollection services, IConfiguration configuration)
    {
        // Resolver
        services.AddScoped<IExternalAuthProviderResolver, ExternalAuthProviderResolver>();

        #region Google
        services.Configure<GoogleOAuthSettings>(configuration.GetSection("Authentication:GoogleOAuth"));

        services.AddRefitClient<IGoogleOAuthApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://oauth2.googleapis.com");
            });

        services.AddScoped<IExternalAuthProvider, GoogleAuthProvider>();
        #endregion

        #region GitHub
        services.Configure<GitHubOAuthSettings>(configuration.GetSection("Authentication:GitHubOAuth"));

        services.AddRefitClient<IGitHubOAuthTokenApi>()
        .ConfigureHttpClient(c => c.BaseAddress = new Uri("https://github.com"));

        services.AddRefitClient<IGitHubUserApi>()
            .ConfigureHttpClient(c =>
            {
                c.BaseAddress = new Uri("https://api.github.com");
                c.DefaultRequestHeaders.UserAgent.ParseAdd("SocialAuthDemo");
                c.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            });

        services.AddScoped<IExternalAuthProvider, GitHubAuthProvider>();
        #endregion

    }


    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<SocialAuthDemoDbContext>(dbContextOptions =>
        {
            dbContextOptions.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }
}
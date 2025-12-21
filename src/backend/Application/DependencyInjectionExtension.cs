using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.RedirectToProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IAuthRedirectToProviderUseCase, AuthRedirectToProviderUseCase>();
        services.AddScoped<IAuthLoginUseCase, AuthLoginUseCase>();
    }

}

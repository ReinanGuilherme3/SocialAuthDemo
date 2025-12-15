using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MudBlazor.Services;
using Refit;
using Web.Api;
using Web.Components;
using Web.Services.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddRefitClient<IAuthApi>()
       .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"]));

builder.Services.AddScoped<IAuthService, AuthService>(); // Registrando a interface e a implementação

// Registro correto do ProtectedLocalStorage no Blazor Server
builder.Services.AddScoped<ProtectedLocalStorage>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

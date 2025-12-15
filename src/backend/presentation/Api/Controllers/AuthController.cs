using Application.UseCases.Auth.Login;
using Application.UseCases.Auth.RedirectToProvider;
using Communication.Requests.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthController : ControllerBase
{

    [HttpGet("redirect-to-provider")]
    public IActionResult RedirectToProvider(
        [FromServices] IAuthRedirectToProviderUseCase usecase,
        [FromQuery] AuthRedirectToProviderRequest request)
    {
        var response = usecase.Execute(request);

        return Ok(response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromServices] IAuthLoginUseCase useCase,
        [FromBody] AuthLoginRequest request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}

using Application.Features.Developers.Commands.CreateDeveloper;
using Application.Features.Developers.Commands.LoginDeveloper;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class AuthController : BaseController
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(CreateDeveloperCommand command)
        {
            var result = await Mediator!.Send(command);
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDeveloperCommand command)
        {
            var result = await Mediator!.Send(command);
            return Ok(result);
        }
    }
}
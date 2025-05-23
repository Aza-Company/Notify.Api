using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NotifyHub.Application.Features.Commands.Auth;
using NotifyHub.Application.Features.Queries.Auth;

namespace NotifyHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command)
    {
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginCommand command)
    {
        var result = await _mediator.Send(command);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }

    /// <summary>
    /// Generates a mobile login token for the user.
    /// </summary>
    /// <returns></returns>
    [HttpGet("mobile-login-token")]
    [Authorize]
    public async Task<IActionResult> GenerateMobileLoginToken()
    {
        var token = await _mediator.Send(new GenerateMobileLoginTokenQuery());

        return token.IsSuccess ? Ok(token) : BadRequest(token);
    }

    /// <summary>
    /// Logs in a user using a mobile login token.
    /// </summary>
    /// <param name="mobileLoginCommand"></param>
    /// <returns></returns>
    [HttpPost("mobile-login")]
    public async Task<IActionResult> MobileLogin([FromQuery] MobileLoginCommand mobileLoginCommand)
    {
        var result = await _mediator.Send(mobileLoginCommand);

        return result.IsSuccess ? Ok(result) : BadRequest(result);
    }
}

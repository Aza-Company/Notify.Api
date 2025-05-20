using MediatR;
using Microsoft.AspNetCore.Mvc;
using NotifyHub.Application.Features.Commands.Sms;

namespace NotifyHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SmsController : ControllerBase
{
    private readonly IMediator _mediator;

    public SmsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Sends an SMS message to the specified recipients.
    /// </summary>
    /// <param name="command"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("send")]
    public async Task<IActionResult> SendSms([FromBody] SendSmsCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(result);
    }
}

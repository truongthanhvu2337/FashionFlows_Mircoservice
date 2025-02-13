using FashionFlows.Payment.Application.UseCases.CheckOut;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FashionFlows.Payment.Api.Controllers;

[Route("api/v1/payment")]
[ApiController]
public class PaymentController : ControllerBase
{
    private ISender _mediator;
    public PaymentController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("check-out")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CheckOutCommand command, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(command, cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }

}

using FashionFlows.Order.Application.UseCases.CreateOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FashionFlows.Order.Api.Controllers;

[Route("api/v1/orders")]
[ApiController]
public class OrderController : Controller
{
    private ISender _mediator;
    public OrderController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateOrderCommand userCommand, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(userCommand, cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }
}

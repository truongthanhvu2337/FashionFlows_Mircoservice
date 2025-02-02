using FashionFlows.Product.Application.UseCases.Products.Commands.CreateProductCommand;
using FashionFlows.Product.Application.UseCases.Products.Commands.DeleteProductCommand;
using FashionFlows.Product.Application.UseCases.Products.Commands.UpdateProductCommand;
using FashionFlows.Product.Application.UseCases.Products.Queries.GetAllProduct;
using FashionFlows.Product.Application.UseCases.Products.Queries.GetProductByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FashionFlows.Product.Api.Controllers;

[Route("api/v1/products")]
[ApiController]
public class ProductController : Controller
{
    private ISender _mediator;
    public ProductController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpPost()]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateProductCommand userCommand, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(userCommand, cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }

    [HttpGet("all")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery, Range(1, int.MaxValue)] int pageNo = 1, int eachPage = 10, CancellationToken cancellationToken = default)
    {
        var result = await _mediator.Send(new GetAllProductsQuery(pageNo, eachPage), cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }

    //[Authorize]
    [HttpGet("")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetById([FromQuery] Guid keyword, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new GetProductByIdQuery(keyword), cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }

    [Authorize]
    [HttpPut("update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] UpdateProductCommand updateUser, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(updateUser, cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }

    //[Authorize("Admin")]
    [HttpDelete("delete")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromQuery] Guid productId, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(new DeleteProductCommand(productId), cancellationToken);
        return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
    }




}

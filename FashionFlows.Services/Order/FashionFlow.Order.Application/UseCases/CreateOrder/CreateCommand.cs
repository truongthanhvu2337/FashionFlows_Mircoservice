using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Order.Application.UseCases.CreateOrder;

public record OrderItemDto(Guid ProductId, int Quantity, int size, decimal UnitPrice);
public record CreateOrderCommand(Guid UserId, List<OrderItemDto> OrderItems) : IRequest<APIResponse>;

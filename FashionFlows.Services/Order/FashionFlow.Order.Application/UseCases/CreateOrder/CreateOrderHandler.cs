using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Messages;
using FashionFlows.BuildingBlock.Domain.Messages.Interface;
using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Order.Domain.Constant;
using FashionFlows.Order.Domain.Entities;
using FashionFlows.Order.Domain.Repositories;
using MassTransit;
using MediatR;

namespace FashionFlows.Order.Application.UseCases.CreateOrder;

public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, APIResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBus _bus;

    public CreateOrderHandler(IOrderRepository orderRepository, IUnitOfWork unitOfWork, IBus bus)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
        _bus = bus;
    }

    public async Task<APIResponse> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Domain.Entities.Order
        {
            Id = Guid.NewGuid(),
            UserId = request.UserId,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            OrderItems = request.OrderItems.Select(oi => new OrderItem
            {
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                Size = oi.size,
                UnitPrice = oi.UnitPrice
            }).ToList()
        };
        await _orderRepository.Add(order);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var newOrderCreated = new CreateOrderMessage
        {
            OrderId = order.Id,
            TotalPrice = order.OrderItems.Sum(a => a.UnitPrice * a.Quantity),
            OrderItemList = order.OrderItems.Select(item => new OrderItemEvent
            {
                Quantity = item.Quantity,
                ProductId = item.ProductId,
                Size = item.Size,
            }).ToList()
        };

        await _bus.Publish<ICreateOrderMessage>(newOrderCreated);
        return new APIResponse
        {
            Data = order,
            Message = "Create order sucessfully",
            StatusResponse = System.Net.HttpStatusCode.OK
        };
    }
}


using FashionFlows.BuildingBlock.Domain.Events.Interface;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Order.Domain.Constant;
using FashionFlows.Order.Domain.Repositories;
using MassTransit;

namespace FashionFlows.Order.Application.Consumers;

public class OrderCompletedEventConsumer : IConsumer<IOrderCompletedEvent>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderCompletedEventConsumer(IOrderRepository orderRepository, IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Consume(ConsumeContext<IOrderCompletedEvent> context)
    {
        var order = await _orderRepository.GetById(context.Message.OrderId);

        if (order != null)
        {
            order.Status = OrderStatus.Processing;

            await _orderRepository.Update(order);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

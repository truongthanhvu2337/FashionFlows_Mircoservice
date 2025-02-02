using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Events.Interface;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Product.Domain.Repositories;
using MassTransit;

namespace FashionFlows.Product.Infrastructure.Consumers;

public class OrderCreatedEventConsumer : IConsumer<IOrderCreatedEvent>
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBus _bus;

    public OrderCreatedEventConsumer(IInventoryRepository inventoryRepository, IUnitOfWork unitOfWork, IBus bus)
    {
        _inventoryRepository = inventoryRepository;
        _unitOfWork = unitOfWork;
        _bus = bus;
    }

    public async Task Consume(ConsumeContext<IOrderCreatedEvent> context)
    {
        var orderItems = context.Message.OrderItemList;
        bool IsExisted = true;

        foreach (var item in orderItems)
        {
            var stock = await _inventoryRepository.GetStockByProductAndSize(item.ProductId, item.Size);
            if (stock == null || stock.StockQuantity < item.Quantity)
            {
                IsExisted = false;
                break;
            }
        }

        if (!IsExisted)
        {
            await _bus.Publish(new StockReservationFailedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                ErrorMessage = "Not enough stock available"
            });

            return;
        }


        foreach (var item in orderItems)
        {
            var stock = await _inventoryRepository.GetStockByProductAndSize(item.ProductId, item.Size);
            if (stock != null)
            {
                stock.StockQuantity -= item.Quantity;

            }
            await _inventoryRepository.Update(stock);
        }

        await _unitOfWork.SaveChangesAsync();

        await _bus.Publish(new StockReservedEvent
        {
            CorrelationId = context.Message.CorrelationId,
            OrderItemList = orderItems
        });
    }
}

using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IStockReservedEvent : CorrelatedBy<Guid>
{
    List<OrderItemEvent> OrderItemList { get; set; }
}

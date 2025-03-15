using FashionFlows.BuildingBlock.Domain.Model;
using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IStockReservedEvent : CorrelatedBy<Guid>
{
    List<OrderItem> OrderItemList { get; set; }
}

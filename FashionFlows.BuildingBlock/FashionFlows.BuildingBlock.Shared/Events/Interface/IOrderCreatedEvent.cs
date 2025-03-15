using FashionFlows.BuildingBlock.Domain.Model;
using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IOrderCreatedEvent : CorrelatedBy<Guid>
{
    List<OrderItem> OrderItemList { get; set; }
}

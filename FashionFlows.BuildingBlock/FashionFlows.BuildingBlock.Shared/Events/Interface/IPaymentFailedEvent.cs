using FashionFlows.BuildingBlock.Domain.Model;
using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IPaymentFailedEvent : CorrelatedBy<Guid>
{
    public List<OrderItem> OrderItemList { get; set; }
}

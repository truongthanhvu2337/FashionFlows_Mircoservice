using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IPaymentCompletedEvent : CorrelatedBy<Guid>
{
}

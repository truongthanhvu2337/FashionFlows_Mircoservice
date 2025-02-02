using MassTransit;

namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IStockReservationFailedEvent : CorrelatedBy<Guid>
{
    string ErrorMessage { get; set; }
}

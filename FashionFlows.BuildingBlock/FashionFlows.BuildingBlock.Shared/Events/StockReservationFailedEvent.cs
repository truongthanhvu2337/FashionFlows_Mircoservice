using FashionFlows.BuildingBlock.Domain.Events.Interface;

namespace FashionFlows.BuildingBlock.Domain.Events;

public class StockReservationFailedEvent : IStockReservationFailedEvent
{
    public Guid CorrelationId { get; set; }
    public string ErrorMessage { get; set; }
}

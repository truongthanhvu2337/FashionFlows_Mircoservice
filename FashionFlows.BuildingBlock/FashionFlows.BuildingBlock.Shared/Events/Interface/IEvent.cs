namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IEvent
{
    Guid CorrelationId { get; }
}

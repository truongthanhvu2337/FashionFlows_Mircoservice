namespace FashionFlows.BuildingBlock.Domain.Events.Interface;

public interface IOrderCompletedEvent
{
    public Guid UserId { get; set; }
    public Guid OrderId { get; set; }
}

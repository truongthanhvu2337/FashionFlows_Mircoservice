using MassTransit;

namespace FashionFlows.StateMachine.Orchestration.StateMachineInstance;

public class OrderStateInstance : SagaStateMachineInstance
{
    public Guid CorrelationId { get; set; }
    public string CurrentState { get; set; }
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime CreateAt { get; set; }
}

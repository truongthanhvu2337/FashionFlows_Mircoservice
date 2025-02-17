using FashionFlows.BuildingBlock.Domain.Constants;
using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Events.Interface;
using FashionFlows.BuildingBlock.Domain.Messages;
using FashionFlows.BuildingBlock.Domain.Messages.Interface;
using FashionFlows.StateMachine.Orchestration.StateMachineInstance;
using MassTransit;

namespace FashionFlows.StateMachine.Orchestration.StateMachine;

public class OrderStateMachine : MassTransitStateMachine<OrderStateInstance>
{
    public State OrderCreated { get; set; }
    public State StockReserved { get; set; }
    public State StockReservationFailed { get; set; }
    public State PaymentCompleted { get; set; }
    public State PaymentFailed { get; set; }
    public State OrderFailed { get; private set; }

    private Event<ICreateOrderMessage> CreateOrderMessage { get; set; }
    public Event<IStockReservedEvent> StockReservedEvent { get; set; }
    public Event<IStockReservationFailedEvent> StockFailedReservation { get; set; }
    public Event<IPaymentCompletedEvent> PaymentCompletedEvent { get; set; }
    public Event<IPaymentFailedEvent> PaymentFailedEvent { get; set; }

    public OrderStateMachine()
    {
        InstanceState(x => x.CurrentState);

        Event(() => CreateOrderMessage, x => x.CorrelateBy<Guid>(y => y.OrderId, z => z.Message.OrderId)
            .SelectId(context => context.Message.OrderId));
        Event(() => StockReservedEvent, x => x.CorrelateById(y => y.Message.CorrelationId));

        Initially(
            When(CreateOrderMessage)
                .Then(context =>
                {
                    context.Saga.UserId = context.Message.UserId;
                    context.Saga.OrderId = context.Message.OrderId;
                    context.Saga.CreateAt = DateTime.UtcNow;
                    context.Saga.TotalPrice = context.Message.TotalPrice;
                })
                .Publish(
                    context => new OrderCreatedEvent
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        OrderItemList = context.Message.OrderItemList
                    })
                .TransitionTo(OrderCreated)
            );
        During(OrderCreated,
            When(StockReservedEvent)
                .TransitionTo(StockReserved)
                .Send(
                    context => new CompletePaymentMessage
                    {
                        CorrelationId = context.Saga.CorrelationId,
                        TotalPrice = context.Saga.TotalPrice,
                        UserId = context.Saga.UserId.ToString(),
                        OrderItemList = context.Message.OrderItemList
                    }),
            When(StockFailedReservation)
                .TransitionTo(OrderFailed)
                .Publish(
                    context => new OrderFailedEvent
                    {
                        OrderId = context.Saga.OrderId,
                        UserId = context.Saga.UserId,
                    }
                )
            );
        During(StockReserved,
            When(PaymentCompletedEvent)
                .TransitionTo(PaymentCompleted)
                .Publish(
                    context => new OrderCompletedEvent
                    {
                        OrderId = context.Saga.OrderId,
                        UserId = context.Saga.UserId
                    })
                .Finalize(),
            When(PaymentFailedEvent)
                .Publish(context => new OrderFailedEvent
                {
                    OrderId = context.Saga.OrderId,
                    UserId = context.Saga.UserId
                })
                .Send(
                    context => new StockRollbackMessage
                    {
                        OrderItemList = context.Message.OrderItemList
                    })
                .TransitionTo(PaymentFailed)
            );
    }

}
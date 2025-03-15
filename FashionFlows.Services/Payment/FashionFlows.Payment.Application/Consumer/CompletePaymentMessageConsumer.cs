using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Domain.Messages.Interface;
using FashionFlows.BuildingBlock.Domain.Model;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Payment.Application.Abstractions;
using FashionFlows.Payment.Domain.Repository;
using MassTransit;

namespace FashionFlows.Payment.Application.Consumer;

public class CompletePaymentMessageConsumer : IConsumer<ICompletePaymentMessage>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IBus _bus;
    private readonly IStripeService _stripeService;

    public CompletePaymentMessageConsumer(IPaymentRepository paymentRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork, IBus bus, IStripeService stripeService)
    {
        _paymentRepository = paymentRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _bus = bus;
        _stripeService = stripeService;
    }

    public async Task Consume(ConsumeContext<ICompletePaymentMessage> context)
    {
        var payload = context.Message;
        var paymentId = Guid.NewGuid();
        var orderItems = payload.OrderItemList.Select(item => new OrderItem
        {
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            Size = item.Size,
            UnitPrice = item.UnitPrice,
        }).ToList();

        var paymentSession = await _stripeService.CheckoutAsync(
            (long)payload.TotalPrice,
            orderItems, 
            paymentId
        );

        //need fixing
        if (paymentSession == null)
        {
            await _bus.Publish(new PaymentFailedEvent
            {
                CorrelationId = context.Message.CorrelationId,
                OrderItemList = context.Message.OrderItemList,
            });
            return;
        }

        var payment = new Domain.Entities.Payment
        {
            Id = paymentId,
            OrderId = payload.OrderId,
            Amount = payload.TotalPrice,
            CheckOutSessionId = paymentSession.Id
        };

        await _paymentRepository.Add(payment);

        var transaction = new Domain.Entities.Transaction
        {
            Id = Guid.NewGuid(),
            PaymentId = payment.Id,
            TransactionType = "Charge",
            Amount = payload.TotalPrice,
            url = paymentSession.Url,
            Status = "Pending"
        };

        await _transactionRepository.Add(transaction);
        await _unitOfWork.SaveChangesAsync();
        await _bus.Publish(new PaymentCompletedEvent
        {
            CorrelationId = context.Message.CorrelationId
        });
        return;
    }
}

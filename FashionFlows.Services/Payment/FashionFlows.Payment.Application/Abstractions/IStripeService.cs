using FashionFlows.BuildingBlock.Domain.Model;
using Stripe;
using Stripe.Checkout;

namespace FashionFlows.Payment.Application.Abstractions;

public interface IStripeService
{
    Task<Session> CheckoutAsync(long amount, List<OrderItem> orderItems, Guid paymentId);
    Task<Refund> ProcessRefundAsync(string paymentIntentId);
    Task<PaymentIntent> RetrievePaymentIntentAsync(string paymentIntentId);
}


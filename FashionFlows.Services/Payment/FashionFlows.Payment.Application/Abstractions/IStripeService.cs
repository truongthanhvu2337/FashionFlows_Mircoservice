using Stripe;
using Stripe.Checkout;

namespace FashionFlows.Payment.Application.Abstractions;

public interface IStripeService
{
    Task<Session> CheckoutAsync(long amount, string productName, int quantity, Guid paymentId);
    Task<Refund> ProcessRefundAsync(string paymentIntentId);
    Task<PaymentIntent> RetrievePaymentIntentAsync(string paymentIntentId);
}


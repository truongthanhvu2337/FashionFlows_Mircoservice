using FashionFlows.BuildingBlock.Domain.Model;
using FashionFlows.Payment.Application.Abstractions;
using FashionFlows.Payment.Infrastructure.ExternalServices.Setting;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Checkout;

namespace FashionFlows.Payment.Infrastructure.ExternalServices;


internal class StripeService : IStripeService
{
    private readonly StripeSettings _stripeSettings;

    public StripeService(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
        StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
    }

    //Simplified Checkout Session
    public async Task<Session> CheckoutAsync(long amount, List<OrderItem> orderItems, Guid paymentId)
    {
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = orderItems.Select(item => new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "usd",
                    UnitAmount = (long)item.UnitPrice,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = item.ProductId.ToString() 
                    }
                },
                Quantity = item.Quantity 
            }).ToList(),
            Metadata = new Dictionary<string, string>
        {
            { "fashionflows_pmid", paymentId.ToString() }
        },
            Mode = "payment",
            SuccessUrl = "https://www.youtube.com/",
            CancelUrl = "https://www.youtube.com/watch?v=WO5l2Siz8uU"
        };

        var service = new SessionService();
        return await service.CreateAsync(options);
    }


    //Process a Refund
    public async Task<Refund> ProcessRefundAsync(string paymentIntentId)
    {
        var refundOptions = new RefundCreateOptions
        {
            PaymentIntent = paymentIntentId
        };

        var refundService = new RefundService();
        return await refundService.CreateAsync(refundOptions);
    }

    //Retrieve Payment Intent
    public async Task<PaymentIntent> RetrievePaymentIntentAsync(string paymentIntentId)
    {
        var service = new PaymentIntentService();
        return await service.GetAsync(paymentIntentId);
    }

    public async Task<Session> CancelCheckOutSessionAsync(string csid)
    {
        var service = new SessionService();
        return await service.ExpireAsync(csid);
    }
}

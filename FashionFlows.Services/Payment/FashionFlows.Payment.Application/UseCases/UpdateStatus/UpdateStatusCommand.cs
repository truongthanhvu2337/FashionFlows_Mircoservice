using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Payment.Application.UseCases.UpdateStatus;

public class UpdateStatusCommand : IRequest<APIResponse>
{
    public string PaymentId { get; set; }
    public string StripePaymentIntentId { get; set; }
    public string Status { get; set; }

    public UpdateStatusCommand(string paymentId, string stripePaymentIntentId, string status)
    {
        PaymentId = paymentId;
        StripePaymentIntentId = stripePaymentIntentId;
        Status = status;
    }
}

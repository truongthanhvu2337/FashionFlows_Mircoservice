using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Payment.Application.UseCases.Refund;

public class RefundCommand : IRequest<APIResponse>
{
    public string paymentIntentId { get; set; }

    public RefundCommand(string paymentIntentId)
    {
        this.paymentIntentId = paymentIntentId;
    }
}

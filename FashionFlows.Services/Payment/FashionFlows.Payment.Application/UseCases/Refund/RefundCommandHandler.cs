using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Payment.Application.Abstractions;
using MediatR;
using System.Net;

namespace FashionFlows.Payment.Application.UseCases.Refund;

public class RefundCommandHandler : IRequestHandler<RefundCommand, APIResponse>
{
    private readonly IStripeService _stripeService;

    public RefundCommandHandler(IStripeService stripeService)
    {
        _stripeService = stripeService;
    }

    public async Task<APIResponse> Handle(RefundCommand request, CancellationToken cancellationToken)
    {
        var paymentSession = await _stripeService.ProcessRefundAsync(request.paymentIntentId);
        return new APIResponse
        {
            Data = paymentSession,
            Message = "Refund successfully.",
            StatusResponse = HttpStatusCode.OK
        };
    }
}

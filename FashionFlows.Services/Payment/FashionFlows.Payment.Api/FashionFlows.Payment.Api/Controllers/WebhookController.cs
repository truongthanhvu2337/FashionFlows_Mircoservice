using FashionFlows.Payment.Application.UseCases.UpdateStatus;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using System.Net;

[Route("api/v1/webhooks/stripe")]
[ApiController]
public class StripeWebhookController : ControllerBase
{
    private readonly IMediator _mediator;

    public StripeWebhookController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> StripeWebhook(CancellationToken cancellationToken = default)
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();


        var stripeEvent = EventUtility.ConstructEvent(json,
            Request.Headers["Stripe-Signature"],
            "whsec_c99a70b220e1a5ecfe7baf16e8a9b3729a59193729f60d90bec6ef40f4e698eb");


        if (stripeEvent.Type == EventTypes.CheckoutSessionCompleted)
        {
            var session = stripeEvent.Data.Object as Session;
            if (session != null)
            {
                string paymentId = session.Id;

                string paymentIntentId = session.PaymentIntentId;

                string status = stripeEvent.Type;

                var updateCommand = new UpdateStatusCommand(paymentId, paymentIntentId, status);
                var result = await _mediator.Send(updateCommand, cancellationToken);
                return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
            }
        }
        else if (stripeEvent.Type == EventTypes.CheckoutSessionExpired)
        {
            var session = stripeEvent.Data.Object as Session;
            string paymentId = session.Id;

            string paymentIntentId = session.PaymentIntentId;

            string status = "Expired";

            var updateCommand = new UpdateStatusCommand(paymentId, paymentIntentId, status);
            var result = await _mediator.Send(updateCommand, cancellationToken);
            return result.StatusResponse != HttpStatusCode.OK ? StatusCode((int)result.StatusResponse, result) : Ok(result);
        }
        return Ok();
    }

}

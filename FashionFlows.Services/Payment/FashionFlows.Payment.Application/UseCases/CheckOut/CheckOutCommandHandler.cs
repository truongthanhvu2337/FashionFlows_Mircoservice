using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Payment.Application.Abstractions;
using FashionFlows.Payment.Domain.Entities;
using FashionFlows.Payment.Domain.Repository;
using MediatR;
using System.Net;
using System.Transactions;

namespace FashionFlows.Payment.Application.UseCases.CheckOut
{
    public class CheckOutCommandHandler : IRequestHandler<CheckOutCommand, APIResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStripeService _stripeService;

        public CheckOutCommandHandler(
            IPaymentRepository paymentRepository,
            ITransactionRepository transactionRepository,
            IUnitOfWork unitOfWork,
            IStripeService stripeService)
        {
            _paymentRepository = paymentRepository;
            _transactionRepository = transactionRepository;
            _unitOfWork = unitOfWork;
            _stripeService = stripeService;
        }

        public async Task<APIResponse> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            //var paymentId = Guid.NewGuid();
            //// Create a Stripe Checkout Session
            //var paymentSession = await _stripeService.CheckoutAsync(request.Amount, "TÉt tingggggggggg", 1, paymentId);

            //if (paymentSession == null)
            //{
            //    return new APIResponse
            //    {
            //        Data = null,
            //        Message = "Failed to create payment session.",
            //        StatusResponse = HttpStatusCode.BadRequest
            //    };
            //}

            //// Create a new Payment record
            //var payment = new Domain.Entities.Payment
            //{
            //    Id = paymentId,
            //    OrderId = request.OrderId,
            //    Amount = request.Amount,
            //    CheckOutSessionId = paymentSession.Id
            //};

            //await _paymentRepository.Add(payment);

            //// Create a new Transaction record for the checkout
            //var transaction = new Domain.Entities.Transaction
            //{
            //    Id = Guid.NewGuid(),
            //    PaymentId = payment.Id,
            //    TransactionType = "Charge",
            //    Amount = request.Amount,
            //    Status = "Pending"
            //};

            //await _transactionRepository.Add(transaction);
            //await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new APIResponse
            {
                Data = null,
                Message = "Checkout session created successfully.",
                StatusResponse = HttpStatusCode.OK
            };
        }
    }
}

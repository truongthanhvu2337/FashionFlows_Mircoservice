using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Payment.Domain.Repository;
using MediatR;
using System.Net;

namespace FashionFlows.Payment.Application.UseCases.UpdateStatus;

public class UpdateStatusCommandHandler : IRequestHandler<UpdateStatusCommand, APIResponse>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateStatusCommandHandler(IPaymentRepository paymentRepository, ITransactionRepository transactionRepository, IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<APIResponse> Handle(UpdateStatusCommand request, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByCheckOutSessionId(request.PaymentId);
        if (payment == null)
        {
            return new APIResponse
            {
                Message = "Payment not found",
                StatusResponse = HttpStatusCode.NotFound
            };
        }

        // Update payment status
        payment.Status = request.Status;
        payment.StripePaymentIntentId = request.StripePaymentIntentId;
        await _paymentRepository.Update(payment);

        // Update transaction status
        var transaction = await _transactionRepository.GetByPaymentId(payment.Id.ToString());
        if (transaction != null)
        {
            transaction.Status = request.Status;
            transaction.StripeTransactionId = request.StripePaymentIntentId;
            await _transactionRepository.Update(transaction);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new APIResponse
        {
            Data = null,
            Message = "Payment and Transaction status updated successfully.",
            StatusResponse = HttpStatusCode.OK
        };
    }
}

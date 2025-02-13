using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Payment.Application.UseCases.CheckOut;

public class CheckOutCommand : IRequest<APIResponse>
{
    public Guid OrderId { get; set; }
    public long Amount { get; set; }
}

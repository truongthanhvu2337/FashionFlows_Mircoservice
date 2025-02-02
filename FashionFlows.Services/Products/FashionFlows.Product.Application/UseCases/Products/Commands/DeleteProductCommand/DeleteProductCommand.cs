using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Product.Application.UseCases.Products.Commands.DeleteProductCommand;

public class DeleteProductCommand : IRequest<APIResponse>
{
    public Guid ProductId { get; set; }

    public DeleteProductCommand(Guid productId)
    {
        ProductId = productId;
    }
}

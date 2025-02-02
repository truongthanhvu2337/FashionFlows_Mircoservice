using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Product.Application.UseCases.Products.Queries.GetProductByIdQuery;

public class GetProductByIdQuery : IRequest<APIResponse>
{
    public Guid ProductId { get; set; }

    public GetProductByIdQuery(Guid productId)
    {
        ProductId = productId;
    }
}

using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Product.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Product.Application.UseCases.Products.Queries.GetProductByIdQuery;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, APIResponse>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<APIResponse> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(request.ProductId);
        if (product == null)
        {
            return new APIResponse
            {
                StatusResponse = HttpStatusCode.NotFound,
                Message = "Product not found."
            };
        }

        return new APIResponse
        {
            StatusResponse = HttpStatusCode.OK,
            Message = "Product retrieved successfully.",
            Data = product
        };
    }
}

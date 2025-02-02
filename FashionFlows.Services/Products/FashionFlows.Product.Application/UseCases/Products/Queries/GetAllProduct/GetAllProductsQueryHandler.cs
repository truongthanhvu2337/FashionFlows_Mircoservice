using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Product.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Product.Application.UseCases.Products.Queries.GetAllProduct
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, APIResponse>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<APIResponse> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAll(request.Page, request.PageSize);

            return new APIResponse
            {
                StatusResponse = HttpStatusCode.OK,
                Message = "Products retrieved successfully.",
                Data = products
            };
        }
    }
}

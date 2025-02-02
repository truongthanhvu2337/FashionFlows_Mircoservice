using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Product.Application.UseCases.Products.Queries.GetAllProduct
{
    public class GetAllProductsQuery : IRequest<APIResponse>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }

        public GetAllProductsQuery(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }
    }
}

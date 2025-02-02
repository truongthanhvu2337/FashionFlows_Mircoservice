using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Product.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Product.Application.UseCases.Products.Commands.UpdateProductCommand;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, APIResponse>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<APIResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
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

        product.Name = request.Name;
        product.Description = request.Description;
        product.Price = request.Price;
        product.Material = request.Material;
        product.Brand = request.Brand;
        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.Update(product);

        return new APIResponse
        {
            StatusResponse = HttpStatusCode.OK,
            Message = "Product updated successfully.",
            Data = product
        };
    }
}

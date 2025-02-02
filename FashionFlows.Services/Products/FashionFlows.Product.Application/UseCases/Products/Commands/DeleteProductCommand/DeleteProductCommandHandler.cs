using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Product.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Product.Application.UseCases.Products.Commands.DeleteProductCommand;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, APIResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<APIResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
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

        product.IsAvailable = false;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new APIResponse
        {
            StatusResponse = HttpStatusCode.OK,
            Message = "Product deleted successfully."
        };
    }
}

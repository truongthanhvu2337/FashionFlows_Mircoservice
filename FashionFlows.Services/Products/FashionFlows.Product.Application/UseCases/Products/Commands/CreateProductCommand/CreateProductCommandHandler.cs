using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Product.Domain.Entities;
using FashionFlows.Product.Domain.Repositories;
using MediatR;
using System.Net;

namespace FashionFlows.Product.Application.UseCases.Products.Commands.CreateProductCommand;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, APIResponse>
{
    private readonly IProductRepository _productRepository;
    private readonly IInventoryRepository _inventoryRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(
        IProductRepository productRepository,
        IInventoryRepository inventoryRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _inventoryRepository = inventoryRepository;
        _categoryRepository = categoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<APIResponse> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var categories = await _categoryRepository.GetCategoriesByIdsAsync(request.CategoryIds);

        if (categories.Count != request.CategoryIds.Count)
        {
            return new APIResponse
            {
                StatusResponse = HttpStatusCode.BadRequest,
                Message = "One or more categories are invalid.",
                Data = null
            };
        }

        // Tạo Product
        var product = new Domain.Entities.Product
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            Color = request.Color,
            Material = request.Material,
            IsAvailable = request.IsAvailable,
            Brand = request.Brand,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            Inventories = request.SizeQuantities.Select(sq => new Inventory
            {
                Size = sq.Key,
                StockQuantity = sq.Value,
                LastUpdated = DateTime.UtcNow
            }).ToList(),
            Categories = categories
        };

        await _productRepository.Add(product);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new APIResponse
        {
            StatusResponse = HttpStatusCode.Created,
            Message = "Product created successfully.",
            Data = product
        };
    }
}

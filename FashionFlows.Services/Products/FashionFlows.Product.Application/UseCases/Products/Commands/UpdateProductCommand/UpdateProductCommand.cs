using FashionFlows.BuildingBlock.Domain.Model.Response;
using MediatR;

namespace FashionFlows.Product.Application.UseCases.Products.Commands.UpdateProductCommand;

public class UpdateProductCommand : IRequest<APIResponse>
{
    public Guid ProductId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Size { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
}

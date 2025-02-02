using FashionFlows.BuildingBlock.Domain.Model.Response;
using FashionFlows.Product.Domain.Constant;
using MediatR;

namespace FashionFlows.Product.Application.UseCases.Products.Commands.CreateProductCommand;

public class CreateProductCommand : IRequest<APIResponse>
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string Color { get; set; } = string.Empty;
    public string Material { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true;
    public string Brand { get; set; } = string.Empty;


    public List<int> CategoryIds { get; set; } = new List<int>();
    public Dictionary<Size, int> SizeQuantities { get; set; } = new Dictionary<Size, int>();
}

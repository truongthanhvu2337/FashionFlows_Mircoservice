using FashionFlows.BuildingBlock.Infrastructure.Repositories;
using FashionFlows.Product.Domain.Entities;

namespace FashionFlows.Product.Domain.Repositories;

public interface IInventoryRepository : IRepository<Domain.Entities.Inventory>
{
    Task<IEnumerable<Inventory>> GetStocksByProductId(Guid productId);
    Task<Inventory?> GetStockByProductAndSize(Guid productId, int size);
    Task<bool> CheckInventory(Guid productId, int size, int requestedQuantity);
    Task AddRange(IEnumerable<Inventory> inventories);
}

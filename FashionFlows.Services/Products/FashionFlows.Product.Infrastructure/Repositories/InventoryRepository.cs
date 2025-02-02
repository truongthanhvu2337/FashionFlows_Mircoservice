using FashionFlows.BuildingBlock.Infrastructure.Repositories.Common;
using FashionFlows.Product.Domain.Entities;
using FashionFlows.Product.Domain.Repositories;
using FashionFlows.Product.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FashionFlows.Product.Infrastructure.Repositories;

public class InventoryRepository : RepositoryBase<Domain.Entities.Inventory, ApplicationDbContext>, IInventoryRepository
{
    private readonly ApplicationDbContext _context;

    public InventoryRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inventory>> GetStocksByProductId(Guid productId)
    {
        return await _context.inventories
                             .Where(i => i.ProductId == productId)
                             .ToListAsync();
    }

    public async Task<Inventory?> GetStockByProductAndSize(Guid productId, int size)
    {
        return await _context.inventories
                             .Where(i => i.ProductId == productId && (int)i.Size == size)
                             .FirstOrDefaultAsync();
    }

    public async Task<bool> CheckInventory(Guid productId, int size, int requestedQuantity)
    {

        var inventory = await _context.inventories
            .FirstOrDefaultAsync(i => i.ProductId == productId && (int)i.Size == size);

        if (inventory == null)
        {
            return false;
        }

        return inventory.StockQuantity >= requestedQuantity;
    }

    public async Task AddRange(IEnumerable<Inventory> inventories)
    {
        await _context.inventories.AddRangeAsync(inventories);
    }
}

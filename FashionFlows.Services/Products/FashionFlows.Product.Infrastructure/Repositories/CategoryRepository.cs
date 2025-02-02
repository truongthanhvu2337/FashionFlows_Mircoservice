using FashionFlows.Product.Domain.Entities;
using FashionFlows.Product.Domain.Repositories;
using FashionFlows.Product.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FashionFlows.Product.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds)
    {
        return await _context.Categories
                             .Where(c => categoryIds.Contains(c.Id))
                             .ToListAsync();
    }
}

using FashionFlows.Product.Domain.Entities;

namespace FashionFlows.Product.Domain.Repositories;

public interface ICategoryRepository
{
    Task<List<Category>> GetCategoriesByIdsAsync(List<int> categoryIds);
}

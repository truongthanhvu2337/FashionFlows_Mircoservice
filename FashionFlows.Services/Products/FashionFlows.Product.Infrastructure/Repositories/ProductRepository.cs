using FashionFlows.BuildingBlock.Infrastructure.Repositories.Common;
using FashionFlows.Product.Domain.Repositories;
using FashionFlows.Product.Infrastructure.Persistence;

namespace FashionFlows.Product.Infrastructure.Repositories;

public class ProductRepository : RepositoryBase<Domain.Entities.Product, ApplicationDbContext>, IProductRepository
{
    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

}

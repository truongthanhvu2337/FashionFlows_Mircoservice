using FashionFlows.BuildingBlock.Infrastructure.Repositories.Common;
using FashionFlows.Order.Domain.Repositories;
using FashionFlows.Order.Infrastructure.Persistence;

namespace FashionFlows.Order.Infrastructure.Repositories;

public class OrderRepository : RepositoryBase<Domain.Entities.Order, ApplicationDbContext>, IOrderRepository
{
    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}

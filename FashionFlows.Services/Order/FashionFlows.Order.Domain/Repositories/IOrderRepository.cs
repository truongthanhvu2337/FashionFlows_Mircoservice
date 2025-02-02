using FashionFlows.BuildingBlock.Infrastructure.Repositories;

namespace FashionFlows.Order.Domain.Repositories;

public interface IOrderRepository : IRepository<Domain.Entities.Order>
{
}

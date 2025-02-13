using FashionFlows.BuildingBlock.Infrastructure.Repositories;
using FashionFlows.Payment.Domain.Entities;

namespace FashionFlows.Payment.Domain.Repository;

public interface ITransactionRepository : IRepository<Transaction>
{
    Task<Transaction?> GetByPaymentId(string id);
}

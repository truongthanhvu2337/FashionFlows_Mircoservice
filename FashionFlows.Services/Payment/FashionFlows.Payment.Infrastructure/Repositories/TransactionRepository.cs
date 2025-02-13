using FashionFlows.BuildingBlock.Infrastructure.Repositories.Common;
using FashionFlows.Payment.Domain.Entities;
using FashionFlows.Payment.Domain.Repository;
using FashionFlows.Payment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FashionFlows.Payment.Infrastructure.Repositories;

public class TransactionRepository : RepositoryBase<Transaction, ApplicationDbContext>, ITransactionRepository
{
    private readonly ApplicationDbContext _context;

    public TransactionRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Transaction?> GetByPaymentId(string id)
    {
        return await _context.Transactions.FirstOrDefaultAsync(a => a.PaymentId.ToString() == id);
    }
}

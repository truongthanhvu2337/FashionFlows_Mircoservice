using FashionFlows.BuildingBlock.Infrastructure.Repositories.Common;
using FashionFlows.Payment.Domain.Repository;
using FashionFlows.Payment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FashionFlows.Payment.Infrastructure.Repositories;

public class PaymentRepository : RepositoryBase<Domain.Entities.Payment, ApplicationDbContext>, IPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public PaymentRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Payment?> GetByStripePaymentIntentId(string stripePaymentIntentId)
    {
        return await _context.Payments.FirstOrDefaultAsync(a => a.StripePaymentIntentId.Equals(stripePaymentIntentId));
    }

    public async Task<Domain.Entities.Payment?> GetByCheckOutSessionId(string stripePaymentIntentId)
    {
        return await _context.Payments.FirstOrDefaultAsync(a => a.CheckOutSessionId.Equals(stripePaymentIntentId));
    }
}

using FashionFlows.BuildingBlock.Infrastructure.Repositories;

namespace FashionFlows.Payment.Domain.Repository;

public interface IPaymentRepository : IRepository<Domain.Entities.Payment>
{
    Task<Domain.Entities.Payment?> GetByStripePaymentIntentId(string stripePaymentIntentId);
    Task<Domain.Entities.Payment?> GetByCheckOutSessionId(string stripePaymentIntentId);
}

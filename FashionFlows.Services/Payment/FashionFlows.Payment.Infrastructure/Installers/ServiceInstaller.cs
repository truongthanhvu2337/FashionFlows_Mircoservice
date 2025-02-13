using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Payment.Application.Abstractions;
using FashionFlows.Payment.Domain.Repository;
using FashionFlows.Payment.Infrastructure.ExternalServices;
using FashionFlows.Payment.Infrastructure.Persistence;
using FashionFlows.Payment.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Payment.Infrastructure.Installers;

public class ServiceInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => (IUnitOfWork)provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IStripeService, StripeService>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ITransactionRepository, TransactionRepository>();
    }
}

using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Order.Domain.Repositories;
using FashionFlows.Order.Infrastructure.Persistence;
using FashionFlows.Order.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Order.Infrastructure.Installers;

public class ServiceInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => (IUnitOfWork)provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IOrderRepository, OrderRepository>();
    }
}

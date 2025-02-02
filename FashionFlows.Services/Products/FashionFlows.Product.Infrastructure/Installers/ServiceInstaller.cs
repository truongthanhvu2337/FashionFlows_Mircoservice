using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Product.Domain.Repositories;
using FashionFlows.Product.Infrastructure.Persistence;
using FashionFlows.Product.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Product.Infrastructure.Installers;

public class ServiceInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => (IUnitOfWork)provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IInventoryRepository, InventoryRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
    }
}

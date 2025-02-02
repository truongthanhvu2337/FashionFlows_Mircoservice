using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Services.Account.Domain.Repositories;
using FashionFlows.Services.Account.Infrastructure.Persistence;
using FashionFlows.Services.Account.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Services.Account.Infrastructure.Installers;

public class ServiceInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped(provider => (IUnitOfWork)provider.GetRequiredService<ApplicationDbContext>());
        services.AddScoped<IUserRepository, UserRepository>();
    }
}

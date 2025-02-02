using FashionFlows.BuildingBlock.Application.Abstraction;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.BuildingBlock.Infrastructure.GenericInstaller;

public static class InstallerExtension
{
    public static void InstallServicesInAssembly<TAssembly>(this IServiceCollection services,
    IConfiguration configuration)
    {
        // Find all implement IInstaller interface and then DI them
        var installers = typeof(TAssembly).Assembly.ExportedTypes.Where(x =>
                typeof(IInstaller).IsAssignableFrom(x) && x is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
        installers.ForEach(installer => installer.InstallerServices(services, configuration));
    }
}

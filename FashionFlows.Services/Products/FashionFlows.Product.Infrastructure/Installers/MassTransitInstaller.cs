using FashionFlows.BuildingBlock.Application.Abstraction;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Product.Infrastructure.Installers;

public class MassTransitInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(typeof(IProductAssembly).Assembly);
            x.UsingRabbitMq((context, cfg) =>
            {

                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("sa");
                    h.Password("sa");
                });
                cfg.ConfigureEndpoints(context);
            });
        });
    }
}

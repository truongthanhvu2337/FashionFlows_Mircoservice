using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.Payment.Application.Consumer;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Payment.Infrastructure.Installers;

public class MassTransitInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {

        services.AddMassTransit(x =>
        {
            x.SetKebabCaseEndpointNameFormatter();

            x.AddConsumers(typeof(IPaymentAssembly).Assembly);
            x.AddConsumer<CompletePaymentMessageConsumer>();
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

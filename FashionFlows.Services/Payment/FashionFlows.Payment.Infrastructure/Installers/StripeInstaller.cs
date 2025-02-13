
using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.Payment.Infrastructure.ExternalServices.Setting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Stripe;

namespace FashionFlows.Payment.Infrastructure.Installers;

public class StripeInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<StripeSettings>(configuration.GetSection("Stripe"));

        var stripeSettings = configuration.GetSection("Stripe").Get<StripeSettings>();
        StripeConfiguration.ApiKey = stripeSettings!.SecretKey;
    }
}

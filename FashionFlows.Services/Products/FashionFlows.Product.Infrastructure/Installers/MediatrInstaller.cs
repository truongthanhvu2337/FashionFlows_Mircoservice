﻿using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.BuildingBlock.Application.Behaviour;
using FashionFlows.Product.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Product.Infrastructure.Installers;

public sealed class MediatrInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(IMediatorAssembly).Assembly);
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviour<,>));
            cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
        });
    }
}


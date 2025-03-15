using FashionFlows.BuildingBlock.Domain.Events;
using FashionFlows.BuildingBlock.Infrastructure.Tracing;
using FashionFlows.StateMachine.Orchestration.Persistence;
using FashionFlows.StateMachine.Orchestration.StateMachine;
using FashionFlows.StateMachine.Orchestration.StateMachineInstance;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

IHost host = Host.CreateDefaultBuilder(args)
    .UseSerilog((hostContext, loggerConfig) =>
    {
        loggerConfig
            .ReadFrom.Configuration(hostContext.Configuration)
            .WriteTo.Console()
            .WriteTo.GrafanaLoki("http://loki:3100");
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddMassTransit(cfg =>
        {
            cfg.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>()
            .EntityFrameworkRepository(r =>
            {
                r.ExistingDbContext<StateMachineDbContext>();

                r.UseSqlServer();
            });

            cfg.UsingRabbitMq((context, cfg) =>
            {

                cfg.Host("rabbitmq", "/", h =>
                {
                    h.Username("sa");
                    h.Password("sa");
                });

                cfg.Publish<OrderCompletedEvent>(x =>
                {
                    x.ExchangeType = ExchangeType.Fanout; 
                });

                cfg.Publish<OrderFailedEvent>(x =>
                {
                    x.ExchangeType = ExchangeType.Fanout;
                });


                //cfg.UseInMemoryOutbox();
                cfg.ConfigureEndpoints(context);
            });
        });

        services.AddDbContext<StateMachineDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                hostContext.Configuration.GetConnectionString("local"),
                b =>
                {
                    b.MigrationsAssembly(typeof(StateMachineDbContext).Assembly.FullName);
                });
        });
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog();
        });
        services.AddOpenTelemetryTracing(hostContext.Configuration);

    })
    .Build();

using var scope = host.Services.CreateScope();
var bus = scope.ServiceProvider.GetRequiredService<IBus>();

await bus.Publish(new OrderCompletedEvent { OrderId = Guid.NewGuid() });
await bus.Publish(new OrderFailedEvent { OrderId = Guid.NewGuid() });

host.Run();
using FashionFlows.StateMachine.Orchestration.Persistence;
using FashionFlows.StateMachine.Orchestration.StateMachine;
using FashionFlows.StateMachine.Orchestration.StateMachineInstance;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost host = Host.CreateDefaultBuilder(args)
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


    })
    .Build();

host.Run();
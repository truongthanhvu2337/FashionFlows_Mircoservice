using FashionFlows.BuildingBlock.Application.Abstraction;
using FashionFlows.Services.Account.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FashionFlows.Services.Account.Infrastructure.Installers;

public sealed class DbInstaller : IInstaller
{
    public void InstallerServices(IServiceCollection services, IConfiguration configuration)
    {
        //var databaseSetting = configuration.GetSection(nameof(PostgresDbSetting)).Get<PostgresDbSetting>();
        //services.AddDbContextPool<IdentityDbContext>(options => options.UseNpgsql(databaseSetting.ConnectionString,
        //    builder =>
        //    {
        //        builder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
        //        builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
        //    }), databaseSetting.PoolSize);

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("local"),
                b =>
                {
                    b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                    b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                });
            options.UseLazyLoadingProxies();
        });
    }
}
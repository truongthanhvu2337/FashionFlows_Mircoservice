using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PersonalScheduling.BuildingBlock.Application.Abstraction;
using System.Reflection;

namespace Anemoi.Identity.Infrastructure.Installers;

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

        var connectionString = configuration.GetConnectionString("local"); // Hoặc "local" tùy vào môi trường
        services.AddDbContextPool<IdentityDbContext>(options => options.UseSqlServer(connectionString,
            builder =>
            {
                builder.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
            }));
    }
}
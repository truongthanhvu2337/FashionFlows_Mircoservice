using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using FashionFlows.Payment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FashionFlows.Payment.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public virtual DbSet<Domain.Entities.Payment> Payments { get; set; } = null!;
    public virtual DbSet<Transaction> Transactions { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
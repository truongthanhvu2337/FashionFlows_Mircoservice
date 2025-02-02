using FashionFlows.StateMachine.Orchestration.StateMachineInstance;
using Microsoft.EntityFrameworkCore;

namespace FashionFlows.StateMachine.Orchestration.Persistence;

public class StateMachineDbContext : DbContext
{
    public StateMachineDbContext(DbContextOptions<StateMachineDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrderStateInstance>().HasKey(s => s.CorrelationId);
    }

    public DbSet<OrderStateInstance> SagaData { get; set; }
}

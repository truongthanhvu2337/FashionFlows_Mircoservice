using FashionFlows.BuildingBlock.Domain.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FashionFlows.Order.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Domain.Entities.Order> Orders { get; set; } = null!;
        public virtual DbSet<Domain.Entities.OrderItem> OrderItems { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

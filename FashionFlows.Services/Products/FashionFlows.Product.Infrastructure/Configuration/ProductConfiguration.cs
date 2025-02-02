using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFlows.Product.Infrastructure.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Domain.Entities.Product>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Product> builder)
    {
        builder.ToTable("Product");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasColumnName("ProductID");

        builder.Property(p => p.Name)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(p => p.Description);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Color)
            .HasMaxLength(50);

        builder.Property(p => p.Material)
            .HasMaxLength(100);

        builder.Property(p => p.Brand)
            .HasMaxLength(100);

        builder.Property(p => p.IsAvailable)
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnType("datetime");

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("datetime");

        builder
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity("ProductCategory");

        builder
            .HasMany(p => p.Inventories)
            .WithOne(i => i.Product)
            .HasForeignKey(i => i.ProductId);
    }
}

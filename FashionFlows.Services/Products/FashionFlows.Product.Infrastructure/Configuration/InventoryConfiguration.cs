using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFlows.Product.Infrastructure.Configuration;

public class InventoryConfiguration : IEntityTypeConfiguration<Domain.Entities.Inventory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Inventory> builder)
    {
        builder.ToTable("Inventory");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.Id)
            .ValueGeneratedOnAdd()
            .HasColumnName("InventoryID");

        builder.Property(i => i.ProductId)
            .IsRequired();

        builder.Property(i => i.Size)
            .HasConversion<string>() //This line converts the data into a string type
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(i => i.StockQuantity)
            .IsRequired()
            .HasDefaultValue(0);

        builder.HasOne(i => i.Product)
            .WithMany(p => p.Inventories)
            .HasForeignKey(i => i.ProductId);
    }
}

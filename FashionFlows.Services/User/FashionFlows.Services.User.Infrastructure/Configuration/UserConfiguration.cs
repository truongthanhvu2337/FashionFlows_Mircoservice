using FashionFlows.Services.Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFlows.Services.Account.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(e => e.UserId);

            builder.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");

            builder.Property(e => e.FullName)
                .HasMaxLength(255);

            builder.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false);

            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .IsRequired();
        }
    }
}

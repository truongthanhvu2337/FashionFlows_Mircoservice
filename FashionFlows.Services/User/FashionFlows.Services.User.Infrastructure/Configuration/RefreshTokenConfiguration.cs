using FashionFlows.Services.Account.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FashionFlows.Services.Account.Infrastructure.Configuration
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshToken");

            builder.HasKey(e => e.RefreshTokenId);

            builder.Property(e => e.RefreshTokenId)
                .HasColumnName("RefreshTokenID")
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnType("datetime");

            builder.Property(e => e.ExpireAt)
                .HasColumnType("datetime");

            builder.Property(e => e.Token)
                .HasMaxLength(300)
                .IsRequired();

            builder.Property(e => e.UserId)
                .HasColumnName("UserID");

            builder.HasOne(d => d.User)
                .WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_RefreshToken_User");
        }
    }
}

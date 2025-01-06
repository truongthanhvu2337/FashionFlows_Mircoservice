using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PersonalScheduling.Services.User.Infrastructure.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<PersonalScheduling.Services.User.Domain.Entities.User>
    {
        public void Configure(EntityTypeBuilder<PersonalScheduling.Services.User.Domain.Entities.User> builder)
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

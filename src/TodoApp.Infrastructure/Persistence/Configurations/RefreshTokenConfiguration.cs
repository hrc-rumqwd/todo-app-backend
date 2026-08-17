using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence.Configurations
{
    internal class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.Property(x => x.CreatedByIp)
                .IsRequired(false);

            builder.Property(x => x.RevokedByIp)
                .IsRequired(false);

            builder.Property(x => x.UserId)
                .IsRequired(true);

            builder.Property(x => x.ExpiresAt)
                .IsRequired(true);
        }
    }
}

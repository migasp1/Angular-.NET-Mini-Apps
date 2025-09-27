using Domain.Entities;
using Domain.Entities.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.UserId);

        builder.HasMany(x => x.Role)
            .WithMany();

        builder.HasMany(x => x.Books)
           .WithOne(x => x.User);

        builder.Property(x => x.Email)
            .HasMaxLength(UserConstraints.EmailMaxLength)
            .IsRequired();

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired();

        builder.Property(x => x.PasswordSalt)
            .IsRequired();
    }
}

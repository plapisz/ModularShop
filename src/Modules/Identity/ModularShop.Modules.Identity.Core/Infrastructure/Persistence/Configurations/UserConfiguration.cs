using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModularShop.Modules.Identity.Core.Entities;
using ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations.Converters;
using ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations.ValueComparers;

namespace ModularShop.Modules.Identity.Core.Infrastructure.Persistence.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Email)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasIndex(x => x.Email)
            .IsUnique();
        
        builder.Property(x => x.Password)
            .HasMaxLength(256)
            .IsRequired();
        
        builder.Property(x => x.Role)
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(x => x.Claims)
            .IsRequired()
            .HasConversion<ClaimsConverter>()
            .Metadata.SetValueComparer(new ClaimsValueComparer());
    }
}
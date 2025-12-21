using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.DataAccess.Mappings;

internal class UserMap : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Nome da tabela
        builder.ToTable("users");

        // Chave primária
        builder.HasKey(u => u.Id);

        // Propriedades
        builder.Property(u => u.Id)
               .HasColumnName("id")
               .IsRequired();

        builder.Property(u => u.ExternalProvider)
               .HasColumnName("external_provider")
               .HasMaxLength(50)
               .IsRequired();

        builder.Property(u => u.ExternalId)
               .HasColumnName("external_id")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(u => u.Email)
               .HasColumnName("email")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(u => u.Name)
               .HasColumnName("name")
               .HasMaxLength(255)
               .IsRequired();

        builder.Property(u => u.Picture)
               .HasColumnName("picture")
               .HasMaxLength(500)
               .IsRequired(false);

        builder.Property(u => u.CreatedAt)
               .HasColumnName("created_at")
               .IsRequired();

        // Constraints
        builder.HasIndex(u => new { u.ExternalProvider, u.ExternalId })
               .IsUnique()
               .HasDatabaseName("uk_users_external_provider_id");

        builder.HasIndex(u => u.Email)
               .IsUnique()
               .HasDatabaseName("uk_users_email");
    }
}

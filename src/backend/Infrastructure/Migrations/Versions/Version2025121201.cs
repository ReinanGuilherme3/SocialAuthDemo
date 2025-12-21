using FluentMigrator;

namespace Infrastructure.Migrations.Versions;

[Migration(DatabaseVersions.REGISTER_TABLE_USER, "Criando tabela usuario")]
public class Version2025062001 : ForwardOnlyMigration
{
    public override void Up()
    {
        Create.Table("users")
            .WithColumn("id").AsGuid().PrimaryKey()
            .WithColumn("external_provider").AsString(50).NotNullable()
            .WithColumn("external_id").AsString(255).NotNullable()
            .WithColumn("email").AsString(255).NotNullable()
            .WithColumn("name").AsString(255).NotNullable()
            .WithColumn("picture").AsString(500).Nullable()
            .WithColumn("created_at").AsDateTime().NotNullable();

        Create.UniqueConstraint("uk_users_external_provider_id")
            .OnTable("users")
            .Columns("external_provider", "external_id");

        Create.UniqueConstraint("uk_users_email")
            .OnTable("users")
            .Column("email");
    }
}
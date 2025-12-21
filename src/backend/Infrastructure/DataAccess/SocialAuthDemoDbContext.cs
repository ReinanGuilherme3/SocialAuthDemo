using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.DataAccess;

public class SocialAuthDemoDbContext : DbContext
{
    public SocialAuthDemoDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SocialAuthDemoDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}

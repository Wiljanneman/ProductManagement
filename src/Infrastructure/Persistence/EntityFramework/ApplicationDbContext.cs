using System.Reflection;
using Application.Commons.Interfaces;
using Domain.Commons;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Persistence.EntityFramework;


public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
{
    private readonly IConfiguration _configuration;


    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(_configuration.GetConnectionString("DefaultConnection"));

    }
    public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<LogEntry> LogEntries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        OnBeforeSaving();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        OnBeforeSaving();
        return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    private void OnBeforeSaving()
    {
        UpdateSoftDelete();
        UpdateTimestamps();
    }

    public async Task CheckAndCreateUser(string email, string password, UserManager<ApplicationUser> _userManager)
    {
        using (var scope = this.GetInfrastructure().CreateScope())
        {
            var userManager = _userManager;
            var existingUser = await userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var newUser = new ApplicationUser { UserName = email, Email = email };
                var result = await userManager.CreateAsync(newUser, password);
                //if (!result.Succeeded)
                //{
                //    // Handle user creation failure
                //}
            }
        }
    }

    private void UpdateSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.IsDeleted = false;
                    break;
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    break;
            }
        }
    }

    private void UpdateTimestamps()
    {
        // TODO: Get real current user id
        var currentUserId = Guid.NewGuid();

        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.Created = DateTime.UtcNow;
                entry.Entity.CreatedBy = currentUserId;
            }

            entry.Entity.LastModified = DateTime.UtcNow;
            entry.Entity.LastModifiedBy = currentUserId;
        }
    }
}

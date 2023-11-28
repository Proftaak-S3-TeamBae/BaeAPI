using BaeDB.Entity;
using Duende.IdentityServer.EntityFramework.Entities;
using Duende.IdentityServer.EntityFramework.Interfaces;

using Duende.IdentityServer.Stores;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace BaeDB;

/// <summary>
/// The database context for the Bae database.
/// </summary>
public class BaeDbContext : DbContext
{
    /// <summary>
    /// The database set for the ai systems
    /// </summary>
    public DbSet<AiSystemEntity> AiSystems { get; init; }

    /// <summary>
    /// The database set for the approved ai system link table
    /// </summary>
    public DbSet<ApprovedAiSystemLinkTable> ApprovedAiSystems { get; init; }

    /// <summary>
    /// The database set for the disapproved ai system link table
    /// </summary>
    public DbSet<DisapprovedAiSystemLinkTable> DisapprovedAiSystems { get; init; }

    /// <summary>
    /// Identity users
    /// </summary>
    /// <param name="database"></param>
    /// <returns></returns>
    public DbSet<IdentityUser> IdentityUsers { get; init; }

    public static BaeDbContext Create(IMongoDatabase database)
        => new(new DbContextOptionsBuilder<BaeDbContext>()
            .UseMongoDB(database.Client, database.DatabaseNamespace.DatabaseName)
            .Options);

    public BaeDbContext(DbContextOptions options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<AiSystemEntity>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ApprovedAiSystemLinkTable>()
            .HasIndex(x => new { x.Id, x.AiSystemId })
            .IsUnique();

        modelBuilder.Entity<DisapprovedAiSystemLinkTable>()
            .HasIndex(x => new { x.Id, x.AiSystemId })
            .IsUnique();
    }
}

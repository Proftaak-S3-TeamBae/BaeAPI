﻿using BaeDB.Entity;
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
    /// The database set for the open ai integration
    /// </summary>
    public DbSet<OpenAiIntegrationEntity> OpenAiIntegration { get; init; }

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
            .HasKey(x => x.Id);

        modelBuilder.Entity<DisapprovedAiSystemLinkTable>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<OpenAiIntegrationEntity>()
            .HasKey(x => x.Id);
    }
}
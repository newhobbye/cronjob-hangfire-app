using System;
using System.Collections.Generic;
using hangfire_jobs_database.Models;
using Microsoft.EntityFrameworkCore;

namespace hangfire_jobs_database.Context;

public partial class LocalDbContext : DbContext
{
    public LocalDbContext()
    {
    }

    public LocalDbContext(DbContextOptions<LocalDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<User> Users { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlite("Data Source=C:\\\\\\\\Users\\\\\\\\Note_Samsung01\\\\\\\\source\\\\\\\\repos\\\\\\\\hangfire-jobs\\\\\\\\hangfire-jobs-database\\\\\\\\DatabaseFile\\\\\\\\localDb.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.HasOne(d => d.User).WithMany(p => p.Addresses).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

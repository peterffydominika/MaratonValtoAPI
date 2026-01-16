using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MaratonValto.Models;

public partial class MaratonvaltoContext : DbContext
{
    public MaratonvaltoContext()
    {
    }

    public MaratonvaltoContext(DbContextOptions<MaratonvaltoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Eredmenyek> Eredmenyeks { get; set; }

    public virtual DbSet<Futok> Futoks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL("server=localhost;database=maratonvalto;user=root;password=;ssl mode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eredmenyek>(entity =>
        {
            entity.HasKey(e => new { e.Kor, e.Futo }).HasName("PRIMARY");

            entity.ToTable("eredmenyek");

            entity.HasIndex(e => e.Futo, "futo");

            entity.Property(e => e.Kor)
                .HasColumnType("int(11)")
                .HasColumnName("kor");
            entity.Property(e => e.Futo)
                .HasColumnType("int(11)")
                .HasColumnName("futo");
            entity.Property(e => e.Ido)
                .HasColumnType("int(11)")
                .HasColumnName("ido");

            entity.HasOne(d => d.FutoNavigation).WithMany(p => p.Eredmenyeks)
                .HasForeignKey(d => d.Futo)
                .HasConstraintName("eredmenyek_ibfk_1");
        });

        modelBuilder.Entity<Futok>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PRIMARY");

            entity.ToTable("futok");

            entity.Property(e => e.Fid)
                .HasColumnType("int(11)")
                .HasColumnName("fid");
            entity.Property(e => e.Csapat)
                .HasColumnType("int(11)")
                .HasColumnName("csapat");
            entity.Property(e => e.Ffi).HasColumnName("ffi");
            entity.Property(e => e.Fnev)
                .HasMaxLength(255)
                .HasColumnName("fnev");
            entity.Property(e => e.Szulev)
                .HasColumnType("int(4)")
                .HasColumnName("szulev");
            entity.Property(e => e.Szulho)
                .HasColumnType("int(2)")
                .HasColumnName("szulho");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

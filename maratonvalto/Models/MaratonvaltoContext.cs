using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace maratonvalto.Models;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=maratonvalto;user=root;password=password;sslmode=none;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Eredmenyek>(entity =>
        {
            entity.HasKey(e => e.Futo).HasName("PRIMARY");

            entity.ToTable("eredmenyek");

            entity.HasIndex(e => e.Futo, "futo");

            entity.HasIndex(e => e.Kor, "kor");

            entity.Property(e => e.Futo).HasColumnName("futo");
            entity.Property(e => e.Ido).HasColumnName("ido");
            entity.Property(e => e.Kor).HasColumnName("kor");
        });

        modelBuilder.Entity<Futok>(entity =>
        {
            entity.HasKey(e => e.Fid).HasName("PRIMARY");

            entity.ToTable("futok");

            entity.Property(e => e.Fid).HasColumnName("fid");
            entity.Property(e => e.Csapat).HasColumnName("csapat");
            entity.Property(e => e.Ffi).HasColumnName("ffi");
            entity.Property(e => e.Fnev)
                .HasMaxLength(60)
                .HasColumnName("fnev");
            entity.Property(e => e.Szulev).HasColumnName("szulev");
            entity.Property(e => e.Szulho).HasColumnName("szulho");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

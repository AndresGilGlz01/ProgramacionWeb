using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace U2_Actividad2.Models.Entities;

public partial class PerrosContext : DbContext
{
    public PerrosContext()
    {
    }

    public PerrosContext(DbContextOptions<PerrosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Caracteristicasfisicas> Caracteristicasfisicas { get; set; }

    public virtual DbSet<Estadisticasraza> Estadisticasraza { get; set; }

    public virtual DbSet<Paises> Paises { get; set; }

    public virtual DbSet<Razas> Razas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;user=andres;password=Siguiente1;database=perros");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Caracteristicasfisicas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("caracteristicasfisicas");

            entity.Property(e => e.Cola).HasMaxLength(500);
            entity.Property(e => e.Color).HasMaxLength(500);
            entity.Property(e => e.Hocico).HasMaxLength(500);
            entity.Property(e => e.Patas).HasMaxLength(500);
            entity.Property(e => e.Pelo).HasMaxLength(500);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Caracteristicasfisicas)
                .HasForeignKey<Caracteristicasfisicas>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_caracteristicasfisicas_1");
        });

        modelBuilder.Entity<Estadisticasraza>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("estadisticasraza");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Estadisticasraza)
                .HasForeignKey<Estadisticasraza>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_estadisticasraza_1");
        });

        modelBuilder.Entity<Paises>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("paises");

            entity.Property(e => e.Nombre).HasMaxLength(45);
        });

        modelBuilder.Entity<Razas>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("razas");

            entity.HasIndex(e => e.IdPais, "pi_idx");

            entity.Property(e => e.Descripcion).HasColumnType("text");
            entity.Property(e => e.Nombre).HasMaxLength(45);
            entity.Property(e => e.OtrosNombres).HasMaxLength(500);

            entity.HasOne(d => d.IdPaisNavigation).WithMany(p => p.Razas)
                .HasForeignKey(d => d.IdPais)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkpai");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

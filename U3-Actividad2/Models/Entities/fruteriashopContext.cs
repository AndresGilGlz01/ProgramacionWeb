using Microsoft.EntityFrameworkCore;

namespace U3_Actividad2.Models.Entities;

public partial class fruteriashopContext : DbContext
{
    public fruteriashopContext()
    {
    }

    public fruteriashopContext(DbContextOptions<fruteriashopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorias> Categorias { get; set; } = null!;
    public virtual DbSet<Productos> Productos { get; set; } = null!;
    public virtual DbSet<Usuarios> Usuarios { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorias>(entity =>
        {
            entity.ToTable("categorias");

            entity.HasIndex(e => e.Nombre, "NombreGrupo");

            entity.Property(e => e.Nombre).HasMaxLength(50);
        });

        modelBuilder.Entity<Productos>(entity =>
        {
            entity.ToTable("productos");

            entity.HasIndex(e => e.IdCategoria, "GruposProductos");

            entity.HasIndex(e => e.IdCategoria, "IdGrupo");

            entity.HasIndex(e => e.Id, "IdProducto");

            entity.Property(e => e.Descripcion).HasColumnType("text");

            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.Property(e => e.Precio).HasPrecision(19, 4);

            entity.Property(e => e.UnidadMedida).HasMaxLength(45);

            entity.HasOne(d => d.IdCategoriaNavigation)
                .WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fk_categorias");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.ToTable("usuarios");

            entity.Property(e => e.Contrasena)
                .HasMaxLength(128)
                .IsFixedLength();

            entity.Property(e => e.Correo).HasMaxLength(255);

            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

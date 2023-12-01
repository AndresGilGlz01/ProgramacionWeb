using Microsoft.EntityFrameworkCore;

namespace U5_Proyecto_Blog.Models.Entities;

public partial class BlogContext : DbContext
{
    public BlogContext()
    {
    }

    public BlogContext(DbContextOptions<BlogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividad> Actividad { get; set; }

    public virtual DbSet<Categoria> Categoria { get; set; }

    public virtual DbSet<Follow> Follow { get; set; }

    public virtual DbSet<Post> Post { get; set; }

    public virtual DbSet<Postcategoria> Postcategoria { get; set; }

    public virtual DbSet<Reporte> Reporte { get; set; }

    public virtual DbSet<Rol> Rol { get; set; }

    public virtual DbSet<Tipoactividad> Tipoactividad { get; set; }

    public virtual DbSet<Usuario> Usuario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.Entity<Actividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("actividad");

            entity.HasIndex(e => e.IdPost, "fkActividad_Post");

            entity.HasIndex(e => e.IdTipo, "fkActividad_TipoActividad");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Fecha).HasColumnType("datetime");
            entity.Property(e => e.IdPost).HasColumnType("int(11)");
            entity.Property(e => e.IdTipo).HasColumnType("int(11)");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Actividad)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkActividad_Post");

            entity.HasOne(d => d.IdTipoNavigation).WithMany(p => p.Actividad)
                .HasForeignKey(d => d.IdTipo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkActividad_TipoActividad");
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("categoria");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Follow>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("follow");

            entity.HasIndex(e => e.IdSeguido, "fkUsuarioUsuario_Usuario1");

            entity.HasIndex(e => e.IdSeguidor, "fkUsuarioUsuario_Usuario2");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdSeguido).HasColumnType("int(11)");
            entity.Property(e => e.IdSeguidor).HasColumnType("int(11)");

            entity.HasOne(d => d.IdSeguidoNavigation).WithMany(p => p.FollowIdSeguidoNavigation)
                .HasForeignKey(d => d.IdSeguido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUsuarioUsuario_Usuario1");

            entity.HasOne(d => d.IdSeguidorNavigation).WithMany(p => p.FollowIdSeguidorNavigation)
                .HasForeignKey(d => d.IdSeguidor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkUsuarioUsuario_Usuario2");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("post");

            entity.HasIndex(e => e.IdCreador, "fkPost_Usuario");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Contenido).HasColumnType("text");
            entity.Property(e => e.FechaActualizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaPublicacion).HasColumnType("datetime");
            entity.Property(e => e.IdCreador).HasColumnType("int(11)");
            entity.Property(e => e.Titulo).HasMaxLength(255);

            entity.HasOne(d => d.IdCreadorNavigation).WithMany(p => p.Post)
                .HasForeignKey(d => d.IdCreador)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkPost_Usuario");
        });

        modelBuilder.Entity<Postcategoria>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("postcategoria");

            entity.HasIndex(e => e.IdCategoria, "fkPostCategoria_Categoria");

            entity.HasIndex(e => e.IdPost, "fkPostCategoria_Post");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdCategoria).HasColumnType("int(11)");
            entity.Property(e => e.IdPost).HasColumnType("int(11)");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Postcategoria)
                .HasForeignKey(d => d.IdCategoria)
                .HasConstraintName("fkPostCategoria_Categoria");

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Postcategoria)
                .HasForeignKey(d => d.IdPost)
                .HasConstraintName("fkPostCategoria_Post");
        });

        modelBuilder.Entity<Reporte>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("reporte");

            entity.HasIndex(e => e.IdPost, "fkReporte_Post");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.IdPost).HasColumnType("int(11)");
            entity.Property(e => e.Motivo).HasMaxLength(255);

            entity.HasOne(d => d.IdPostNavigation).WithMany(p => p.Reporte)
                .HasForeignKey(d => d.IdPost)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fkReporte_Post");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Tipoactividad>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tipoactividad");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Nombre).HasMaxLength(255);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuario");

            entity.HasIndex(e => e.IdRol, "fkUsuario_Rol");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.Activo)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmailConfirmed)
                .HasDefaultValueSql("b'0'")
                .HasColumnType("bit(1)");
            entity.Property(e => e.IdRol).HasColumnType("int(11)");
            entity.Property(e => e.NombreUsuario).HasMaxLength(255);
            entity.Property(e => e.Password).HasMaxLength(255);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuario)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("fkUsuario_Rol");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Microsoft.EntityFrameworkCore;

namespace Villancicos.Models;

public partial class villancicosContext : DbContext
{
    public villancicosContext()
    {
    }

    public villancicosContext(DbContextOptions<villancicosContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Villancico> Villancico { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Villancico>(entity =>
        {
            entity.ToTable("villancico");

            entity.Property(e => e.Id).HasColumnType("int(11)");

            entity.Property(e => e.Anyo).HasColumnType("int(11)");

            entity.Property(e => e.Compositor).HasColumnType("varchar(50)");

            entity.Property(e => e.Letra)
                .IsRequired()
                .HasColumnType("text");

            entity.Property(e => e.Nombre)
                .IsRequired()
                .HasColumnType("varchar(100)");

            entity.Property(e => e.VideoUrl)
                .IsRequired()
                .HasColumnName("VideoURL")
                .HasColumnType("varchar(200)");
        });
    }
}

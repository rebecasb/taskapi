using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskAPI.Models;

namespace TaskAPI.Data;

public class TaskContext : DbContext
{
    public TaskContext(DbContextOptions<TaskContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Tarea> Tareas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("Usuario");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            entity.HasIndex(e => e.Email)
                .IsUnique();

            entity.HasIndex(e => e.NombreUsuario)
                .IsUnique();
        });


        modelBuilder.Entity<Tarea>(entity =>
        {
            entity.ToTable("Tarea");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            entity.Property(t => t.Estado)
            .HasConversion<string>();

            entity.HasOne(d => d.Usuario)
                .WithMany(p => p.Tareas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(e => e.Estado);
            entity.HasIndex(e => e.FechaVencimiento);
        });


    }
}

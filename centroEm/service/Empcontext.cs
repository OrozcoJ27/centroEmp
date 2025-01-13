using Microsoft.EntityFrameworkCore;

public class Empcontext : DbContext
{
    public DbSet<Emprendedor> Emprendedores { get; set; }

    public Empcontext(DbContextOptions<Empcontext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Mapear la tabla "Emprendedores" a la clase "Emprendedor"
        modelBuilder.Entity<Emprendedor>().ToTable("emprendedores");

        // Aquí puedes agregar configuraciones adicionales si las necesitas.
    }
}

using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<UsuarioModel> Usuario { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("DataSource=app.db;Cache=Shared");
}

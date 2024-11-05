using Microsoft.EntityFrameworkCore;
using PersonCRUD.Model;

namespace PersonCRUD.Data;

public class AppDbContext : DbContext
{
    public DbSet<Person> Peoples { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=person.sqlite");
        base.OnConfiguring(optionsBuilder);
    }
}

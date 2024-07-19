using EntityFrameWorkHomeWork01.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EntityFrameWorkHomeWork01.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Item> Items { get; set; }

    public ApplicationDbContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var configuration = App.Provider.GetService<ConfigurationBuilder>()!;

        configuration.SetBasePath(@"C:\Users\User\RiderProjects\EntityFrameWorkHomeWork01\EntityFrameWorkHomeWork01\Config");

        var cs = configuration.AddJsonFile(@"package.json").Build();

        optionsBuilder.UseSqlServer(cs.GetConnectionString("DefaultConnection"));
    }
}
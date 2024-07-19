using System.IO;
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

        var path = Directory.GetCurrentDirectory();

        var newPath = Path.GetFullPath(Path.Combine(path, @"..\..\..\Config"));

        configuration.SetBasePath(newPath);

        var cs = configuration.AddJsonFile(@"package.json").Build();

        optionsBuilder.UseSqlServer(cs.GetConnectionString("DefaultConnection"));
    }
}
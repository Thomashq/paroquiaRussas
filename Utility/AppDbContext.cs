using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace paroquiaRussas.Utility;
public class AppDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public AppDbContext(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Configuration.GetConnectionString("conn"));
    }

    public DbSet<User> User { get; set;}
}

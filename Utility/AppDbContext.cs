using Microsoft.EntityFrameworkCore;
using paroquiaRussas.Models;
using Microsoft.Extensions.Configuration;
using System;

namespace paroquiaRussas.Utility;
public class AppDbContext : DbContext
{   
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

    public DbSet<Users>? Users { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        BaseModel.Configure(builder);
    }
}

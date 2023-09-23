using AuthLab.DataAccess.Models;
using AuthLab.Domain.Entities;
using AuthLab.Security;
using Microsoft.EntityFrameworkCore;

namespace AuthLab.DataAccess.EfCore;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "Admin",
                Role = "Admin",
                HashedPassword = SecurePasswordHasher.Hash("123123"),
            });
    }
}

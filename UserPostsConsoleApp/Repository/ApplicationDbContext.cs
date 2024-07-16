using Microsoft.EntityFrameworkCore;

namespace UserPostsConsoleApp.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public DbSet<Account> Accounts {get; set;}
    public DbSet<Posts> Posts {get; set;}
}
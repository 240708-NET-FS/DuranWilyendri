using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace UserPostsConsoleApp.Entities;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public ApplicationDbContext(){}

    public DbSet<Account> Accounts {get; set;}
    public DbSet<Posts> Posts {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // base.OnConfiguring(optionsBuilder);
        if (!optionsBuilder.IsConfigured) {
            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var connectionString = configurationRoot.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // base.OnModelCreating(modelBuilder);

        // modelBuilder.Entity<Posts>().HasOne(a => a.Account).WithMany(p => p.Posts).HasForeignKey(a => a.AccountID);
        modelBuilder.Entity<Posts>()
                .HasKey(p => p.PostID);  // Explicitly define primary key

        modelBuilder.Entity<Posts>()
                .HasOne(p => p.Account)
                .WithMany(a => a.Posts)
                .HasForeignKey(p => p.AccountID);


        //Could also be done from the perspective of Account. Notice still referring the AccountID as a foreing key for Posts
        // modelBuilder.Entity<Account>().HasMany(p => p.Posts).WithOne(a => a.Account).HasForeignKey(a => a.AccountID);
    }
}
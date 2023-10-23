using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class ApplicationDbContext:DbContext
{
   
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
    
    public DbSet<Domain.Model.Category.Category>Categories { get; set; }
    public DbSet<Domain.Model.Item.Item>Items { get; set; }
}
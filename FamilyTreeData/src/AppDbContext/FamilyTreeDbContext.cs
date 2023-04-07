using FamilyTreeData.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FamilyTreeData.AppDbContext;

public class FamilyTreeDbContext : DbContext
{
    public FamilyTreeDbContext(DbContextOptions<FamilyTreeDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.ApplyConfiguration(new PersonConfiguration());
    }
}
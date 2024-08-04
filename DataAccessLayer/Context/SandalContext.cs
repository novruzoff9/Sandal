using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context;

public class SandalContext : DbContext
{

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-ALE5B86;initial catalog=SandalDb;integrated security=true");
    }
    public DbSet<Category> Categories { get; set; }
    public DbSet<SubCategory> SubCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });
        modelBuilder.Entity<SubCategory>(entity =>
        {
            entity.HasOne(e => e.Category).WithMany(e => e.SubCategories)
            .HasForeignKey(e => e.CategoryId)
            .HasConstraintName("FK_SubCategories_Categories");
        });
    }
}

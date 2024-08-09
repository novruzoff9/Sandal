using DataAccessLayer.Configurations;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurationsl;

public class CategoryMapping : BaseEntityMapping<Category>
{
    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);
        builder.Property(e => e.Name).HasMaxLength(50);

        builder.HasOne(e => e.ParentCategory)
            .WithMany(e => e.SubCategories)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.ParentCategory);

        builder.HasMany(e => e.Products)
            .WithOne(e => e.Category)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.CategoryId);
    }
}

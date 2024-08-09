using DataAccessLayer.Configurations;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurationsl;

public class ProductMapping : BaseEntityMapping<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        builder.HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.CategoryId);
    }
}

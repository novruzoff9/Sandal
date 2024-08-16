using DataAccessLayer.Configurations;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;

public class ProductMapping : BaseEntityMapping<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Price)
            .HasColumnType("decimal(18,2)");

        builder.Property(e => e.Rating)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(e => e.Category)
            .WithMany(e => e.Products)
            .OnDelete(DeleteBehavior.Restrict)
            .HasForeignKey(e => e.CategoryId);
    }
}

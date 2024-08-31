using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurations;

public class UserFavoriteProductMapping : IEntityTypeConfiguration<UserFavoriteProduct>
{
    public void Configure(EntityTypeBuilder<UserFavoriteProduct> builder)
    {
        builder.HasKey(e => new { e.ProductId , e.UserId});

        builder.HasOne(uf => uf.User)
            .WithMany(u => u.FavoriteProducts)
            .HasForeignKey(uf => uf.UserId);

        builder.HasOne(uf => uf.Product)
            .WithMany(u => u.UserFavorites)
            .HasForeignKey(uf => uf.ProductId);
    }
}

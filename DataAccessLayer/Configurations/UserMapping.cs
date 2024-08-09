using DataAccessLayer.Configurations;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccessLayer.Configurationsl;

public class UserMapping : BaseEntityMapping<User>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);
        builder.HasIndex(e => e.UserName).IsUnique();
        builder.HasIndex(e => e.Email).IsUnique();
    }
}

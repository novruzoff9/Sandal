using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configurations;

public class FAQMapping : BaseEntityMapping<FAQ>
{
    public override void Configure(EntityTypeBuilder<FAQ> builder)
    {
        base.Configure(builder);

        builder.Property(e => e.Category)
            .HasConversion<int>()
            .HasColumnType("int")
            .IsRequired();

        builder.Property(e => e.Question)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(e=>e.Answer)
            .HasMaxLength(1000)
            .IsRequired();
    }
}

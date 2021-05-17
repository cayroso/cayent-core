using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Data.Components.Brands
{
    public class BrandBase
    {
        public string BrandId { get; set; }
        public string Name { get; set; }
    }

    public class BrandBaseConfiguration : EntityBaseConfiguration<BrandBase>
    {
        public override void Configure(EntityTypeBuilder<BrandBase> b)
        {
            b.ToTable("Brand");
            b.HasKey(e => e.BrandId);

            b.Property(e => e.BrandId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
        }
    }
}

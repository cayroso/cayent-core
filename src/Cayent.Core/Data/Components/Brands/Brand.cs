﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Brands
{
    internal class BrandBase
    {
        public string BrandId { get; set; }
        public string Name { get; set; }
    }

    internal class BrandBaseConfiguration : EntityBaseConfiguration<BrandBase>
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

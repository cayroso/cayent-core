﻿using Cayent.Core.Data.Components.Items;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Stocks
{
    internal class StockBase
    {
        public string StockId { get; set; }

        public string ItemId { get; set; }
        public virtual ItemBase Item { get; set; }

        public string Serial { get; set; }
    }

    internal class StockBaseConfiguration : EntityBaseConfiguration<StockBase>
    {
        public override void Configure(EntityTypeBuilder<StockBase> b)
        {
            b.ToTable("Stock");
            b.HasKey(e => e.StockId);
            b.HasIndex(e => new { e.ItemId, e.Serial }).IsUnique();

            b.Property(e => e.StockId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ItemId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Serial).HasMaxLength(NameMaxLength);
        }
    }
}

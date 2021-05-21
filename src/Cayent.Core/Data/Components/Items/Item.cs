using Cayent.Core.Data.Components.ItemGroups;
using Cayent.Core.Data.Components.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Cayent.Core.Data.Components.Items
{
    public class ItemBase
    {
        public string ItemId { get; set; }

        public string ItemGroupId { get; set; }
        public virtual ItemGroupBase ItemGroup { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        //public virtual ICollection<StockBase> Stocks { get; set; } = new List<StockBase>();
    }

    public class ItemBaseConfiguration : EntityBaseConfiguration<ItemBase>
    {
        public override void Configure(EntityTypeBuilder<ItemBase> b)
        {
            b.ToTable("Item");
            b.HasKey(e => e.ItemId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ItemId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ItemGroupId).HasMaxLength(KeyMaxLength);
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.Description).HasMaxLength(DescMaxLength);
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);

            //b.HasMany(e => e.Stocks)
            //    .WithOne(d => d.Item)
            //    .HasForeignKey(fk => fk.ItemId);

        }
    }
}

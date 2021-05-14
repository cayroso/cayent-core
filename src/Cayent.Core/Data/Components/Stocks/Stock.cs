using Cayent.Core.Data.Components.Items;
using Cayent.Core.Data.Components.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Cayent.Core.Data.Components.Stocks
{
    public abstract class StockBase
    {
        public string StockId { get; set; }

        public string ItemId { get; set; }
        public virtual ItemBase Item { get; set; }

        public string Serial { get; set; }

        public virtual ICollection<StoreStockBase> StoreStocks { get; set; } = new List<StoreStockBase>();
    }

    public class StockBaseConfiguration : EntityBaseConfiguration<StockBase>
    {
        public override void Configure(EntityTypeBuilder<StockBase> b)
        {
            b.ToTable("Stock");
            b.HasKey(e => e.StockId);
            b.HasIndex(e => e.Serial).IsUnique();

            b.Property(e => e.StockId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ItemId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Serial).HasMaxLength(NameMaxLength).IsRequired();
            //b.Property(e => e.Description).HasMaxLength(DescMaxLength);
            //b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            //b.HasQueryFilter(e => e.Active);

            b.HasMany(e => e.StoreStocks)
                .WithOne(d => d.Stock)
                .HasForeignKey(fk => fk.StockId);
        }
    }
}

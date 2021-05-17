using Cayent.Core.Data.Components.Stocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Stores
{
    public class StoreStockBase
    {
        public string StoreId { get; set; }
        public virtual StoreBase Store { get; set; }

        public string StockId { get; set; }
        public virtual StockBase Stock { get; set; }

        public int Quantity { get; set; }
        public int SafetyQuantity { get; set; }
    }

    public class StoreStockBaseConfiguration : EntityBaseConfiguration<StoreStockBase>
    {
        public override void Configure(EntityTypeBuilder<StoreStockBase> b)
        {
            b.ToTable("StoreStock");
            b.HasKey(e => new { e.StoreId, e.StockId });

            b.Property(e => e.StoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.StockId).HasMaxLength(KeyMaxLength).IsRequired();

            //b.HasQueryFilter(e => e.Store.Active && e.Stock.Active);
        }
    }
}

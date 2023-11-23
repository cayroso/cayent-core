using Cayent.Core.Data.Components.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Stores
{
    internal class StoreProductBase
    {        
        public string StoreId { get; set; }
        public virtual StoreBase Store { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }
    }

    internal class StoreProductBaseConfiguration : EntityBaseConfiguration<StoreProductBase>
    {
        public override void Configure(EntityTypeBuilder<StoreProductBase> b)
        {            
            b.ToTable("StoreProduct");
            b.HasKey(e => new { e.StoreId, e.ProductId });

            b.Property(e => e.StoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();            
        }
    }

}

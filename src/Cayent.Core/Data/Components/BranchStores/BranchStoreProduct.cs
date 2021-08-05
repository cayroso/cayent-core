using Cayent.Core.Data.Components;
using Cayent.Core.Data.Components.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace Cayent.Core.Data.Components.BranchStores
{
    internal abstract class BranchStoreProductBase
    {        
        public string BranchStoreId { get; set; }
        public virtual BranchStoreBase BranchStore { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }
    }

    internal class BranchStoreProductBaseConfiguration: EntityBaseConfiguration<BranchStoreProductBase>
    {
        public override void Configure(EntityTypeBuilder<BranchStoreProductBase> b)
        {            
            b.ToTable("BranchStoreProduct");
            b.HasKey(e => new { e.BranchStoreId, e.ProductId });

            b.Property(e => e.BranchStoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.BranchStore.Active && e.Product.Active);
        }
    }

}

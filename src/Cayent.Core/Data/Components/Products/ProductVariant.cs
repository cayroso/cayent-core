using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Products
{
    public abstract class ProductVariantBase
    {
        public string ProductVariantId { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }
    }

    public class ProductVariantBaseConfiguration : EntityBaseConfiguration<ProductVariantBase>
    {
        public override void Configure(EntityTypeBuilder<ProductVariantBase> b)
        {
            b.ToTable("ProductVariant");
            b.HasKey(e => e.ProductVariantId);
            
            b.Property(e => e.ProductVariantId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();            
        }
    }
}

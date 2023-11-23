using Cayent.Core.Data.Components.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Products
{
    internal class ProductVariantBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductVariantId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductBase> Products { get; set; } = new List<ProductBase>();
        public virtual ICollection<PromotionProductFilterBase> PromotionProductFilters { get; set; } = new List<PromotionProductFilterBase>();

    }

    internal class ProductVariantBaseConfiguration : EntityBaseConfiguration<ProductVariantBase>
    {
        public override void Configure(EntityTypeBuilder<ProductVariantBase> b)
        {
            b.ToTable("ProductVariant");
            b.HasKey(e => e.ProductVariantId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ProductVariantId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();

            //b.HasMany(e => e.Products)
            //    .WithOne(d => d.ProductVariant)
            //    .HasForeignKey(fk => fk.ProductVariantId);

            //b.HasMany(e => e.PromotionProductFilters)
            //    .WithOne(d => d.ProductVariant)
            //    .HasForeignKey(fk => fk.ProductVariantId);
        }
    }
}

using Cayent.Core.Data.Components.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Products
{
    public abstract class ProductGroupBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductGroupId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductBase> Products { get; set; } = new List<ProductBase>();
        public virtual ICollection<PromotionProductFilterBase> PromotionProductFilters { get; set; } = new List<PromotionProductFilterBase>();

    }

    public class ProductGroupBaseConfiguration : EntityBaseConfiguration<ProductGroupBase>
    {
        public override void Configure(EntityTypeBuilder<ProductGroupBase> b)
        {
            b.ToTable("ProductGroup");
            b.HasKey(e => e.ProductGroupId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ProductGroupId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();

            b.HasMany(e => e.Products)
                .WithOne(d => d.ProductGroup)
                .HasForeignKey(fk => fk.ProductGroupId);

            b.HasMany(e => e.PromotionProductFilters)
                .WithOne(d => d.ProductGroup)
                .HasForeignKey(fk => fk.ProductGroupId);
        }
    }
}

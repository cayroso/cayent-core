using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using Cayent.Core.Data.Components.Products;

namespace Cayent.Core.Data.Components.Promotions
{
    public abstract class PromotionProductFilterBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PromotionProductFilterId { get; set; }

        public string PromotionId { get; set; }
        public virtual PromotionBase Promotion { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }

        public string ProductGroupId { get; set; }
        public virtual ProductGroupBase ProductGroup { get; set; }

    }

    public class PromotionProductFilterBaseConfiguration : EntityBaseConfiguration<PromotionProductFilterBase>
    {
        public override void Configure(EntityTypeBuilder<PromotionProductFilterBase> b)
        {
            b.ToTable("PromotionProductFilter");
            b.HasKey(e => e.PromotionProductFilterId);

            b.Property(e => e.PromotionProductFilterId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.PromotionId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength);
            b.Property(e => e.ProductGroupId).HasMaxLength(KeyMaxLength);

            b.HasQueryFilter(e => e.Promotion.Active);
        }
    }
}

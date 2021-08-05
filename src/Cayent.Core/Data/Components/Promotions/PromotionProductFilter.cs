using Cayent.Core.Data.Components;
using Cayent.Core.Data.Components.ItemGroups;
using Cayent.Core.Data.Components.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Promotions
{
    internal abstract class PromotionProductFilterBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string PromotionProductFilterId { get; set; }

        public string PromotionId { get; set; }
        public virtual PromotionBase Promotion { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }

        public string ItemGroupId { get; set; }
        public virtual ItemGroupBase ItemGroup { get; set; }

    }

    internal class PromotionProductFilterBaseConfiguration : EntityBaseConfiguration<PromotionProductFilterBase>
    {
        public override void Configure(EntityTypeBuilder<PromotionProductFilterBase> b)
        {
            b.ToTable("PromotionProductFilterId");
            b.HasKey(e => e.PromotionProductFilterId);

            b.Property(e => e.PromotionProductFilterId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.PromotionId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength);
            b.Property(e => e.ItemGroupId).HasMaxLength(KeyMaxLength);

            b.HasQueryFilter(e => e.Promotion.Active);
        }
    }
}

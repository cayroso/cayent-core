using Cayent.Core.Data.Components.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Orders.OrderPromotions
{
    public class OrderPromotionBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderPromotionId { get; set; }

        public string OrderId { get; set; }
        public virtual OrderBase Order { get; set; }

        public string PromotionId { get; set; }
        public virtual PromotionBase Promotion { get; set; }

        public virtual ICollection<OrderPromotionLineItemBase> OrderPromotionLineItems { get; set; }
    }

    public class OrderPromotionBaseConfiguration : EntityBaseConfiguration<OrderPromotionBase>
    {
        public override void Configure(EntityTypeBuilder<OrderPromotionBase> b)
        {
            b.ToTable("OrderPromotion");
            b.HasKey(e => e.OrderPromotionId);

            b.Property(e => e.OrderPromotionId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.PromotionId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Promotion.Active);

            b.HasMany(e => e.OrderPromotionLineItems)
                .WithOne(d => d.OrderPromotion)
                .HasForeignKey(fk => fk.OrderPromotionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using Cayent.Core.Data.Components.Orders.OrderLineItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Orders.OrderPromotions
{
    internal class OrderPromotionLineItemBase
    {
        public string OrderPromotionId { get; set; }
        public virtual OrderPromotionBase OrderPromotion { get; set; }

        public string OrderLineItemId { get; set; }
        public virtual OrderLineItemBase OrderLineItem { get; set; }

        public double Quantity { get; set; }
    }

    internal class OrderPromotionLineItemBaseConfiguration : EntityBaseConfiguration<OrderPromotionLineItemBase>
    {
        public override void Configure(EntityTypeBuilder<OrderPromotionLineItemBase> b)
        {
            b.ToTable("OrderPromotionLineItem");
            b.HasKey(e => new { e.OrderPromotionId, e.OrderLineItemId });

            b.Property(e => e.OrderPromotionId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderLineItemId).HasMaxLength(KeyMaxLength).IsRequired();
        }
    }
}

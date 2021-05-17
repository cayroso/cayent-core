using Cayent.Core.Data.Components.Orders.OrderLineItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Orders.OrderShipments
{
    public class OrderShipmentLineItemBase
    {
        public string OrderShipmentId { get; set; }
        public virtual OrderShipmentBase OrderShipment { get; set; }

        public string OrderLineItemId { get; set; }
        public virtual OrderLineItemBase OrderLineItem { get; set; }

        public uint Quantity { get; set; }
    }

    public class OrderShipmentLineItemBaseConfiguration : EntityBaseConfiguration<OrderShipmentLineItemBase>
    {
        public override void Configure(EntityTypeBuilder<OrderShipmentLineItemBase> b)
        {
            b.ToTable("OrderShipmentLineItem");
            b.HasKey(e => new { e.OrderShipmentId, e.OrderLineItemId });

            b.Property(e => e.OrderShipmentId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderLineItemId).HasMaxLength(KeyMaxLength).IsRequired();
        }
    }
}

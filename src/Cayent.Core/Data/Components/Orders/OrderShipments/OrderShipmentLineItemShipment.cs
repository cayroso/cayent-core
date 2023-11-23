using Cayent.Core.Data.Components.Orders.OrderLineItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Orders.OrderShipments
{
    internal abstract class OrderShipmentLineItemShipmentBase
    {
        public string OrderShipmentId { get; set; }
        public virtual OrderShipmentBase OrderShipment { get; set; }

        public string OrderLineItemId { get; set; }
        public virtual OrderLineItemBase OrderLineItem { get; set; }

        public uint Quantity { get; set; }
    }

    internal class OrderShipmentLineItemShipmentBaseConfiguration : EntityBaseConfiguration<OrderShipmentLineItemShipmentBase>
    {
        public override void Configure(EntityTypeBuilder<OrderShipmentLineItemShipmentBase> b)
        {
            b.ToTable("OrderShipmentLineItemShipment");
            b.HasKey(e => new { e.OrderShipmentId, e.OrderLineItemId });

            b.Property(e => e.OrderShipmentId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderLineItemId).HasMaxLength(KeyMaxLength).IsRequired();
        }
    }
}

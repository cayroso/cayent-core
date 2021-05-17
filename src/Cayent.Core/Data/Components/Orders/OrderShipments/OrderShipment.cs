using System;
using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Cayent.Core.Data.Components.Orders.OrderShipments
{
    public class OrderShipmentBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderShipmentId { get; set; }

        public string OrderId { get; set; }
        public virtual OrderBase Order { get; set; }

        public string TrackingNumber { get; set; }


        DateTime _createdDateTime;
        public DateTime CreatedDateTime
        {
            get => _createdDateTime.AsUtc();
            set => _createdDateTime = value.Truncate();
        }

        DateTime _deliveryDate;
        public DateTime DeliveryDate
        {
            get => _deliveryDate.AsUtc();
            set => _deliveryDate = value.Truncate();
        }

        /// <summary>
        /// Delivered, ReadyForPickup, Shipped, Undeliverable
        /// </summary>
        public string Status { get; set; }

        public virtual ICollection<OrderShipmentLineItemBase> OrderShipmentLineItems { get; set; } = new List<OrderShipmentLineItemBase>();
    }

    public class OrderShipmentBaseConfiguration : EntityBaseConfiguration<OrderShipmentBase>
    {
        public override void Configure(EntityTypeBuilder<OrderShipmentBase> b)
        {
            b.ToTable("OrderShipment");
            b.HasKey(e => e.OrderShipmentId);

            b.Property(e => e.OrderShipmentId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.TrackingNumber).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasMany(e => e.OrderShipmentLineItems)
                .WithOne(d => d.OrderShipment)
                .HasForeignKey(fk => fk.OrderShipmentId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}

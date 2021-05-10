using System;
using System.Collections.Generic;
using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Components.Orders.OrderShipments
{
    public abstract class OrderShipmentBase
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
       
        //public virtual ICollection<OrderShipmentLineItemShipmentBase> LineItems { get; set; }
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

        }
    }
}

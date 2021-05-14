using System;
using Data.Enums;
using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cayent.Core.Data.Components.Customers;
using Cayent.Core.Data.Components.Stores;
using Cayent.Core.Data.Components.Settings;
using System.Collections.Generic;
using Cayent.Core.Data.Components.Orders.OrderLineItems;

namespace Cayent.Core.Data.Components.Orders
{
    public abstract class OrderBase
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public virtual CustomerBase Customer { get; set; }

        public string StoreId { get; set; }
        public virtual StoreBase Store { get; set; }

        public string ShippingSettingId { get; set; }
        public virtual ShippingSettingBase ShippingSetting { get; set; }

        public virtual OrderDeliveryAddressBase DeliveryAddress { get; set; }

        public string Number { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }

        public EnumPaymentMethod PaymentMethod { get; set; }

        public abstract void UpdateStatus(EnumOrderStatus status, string userId, string note);


        DateTime _orderDateTime;
        public DateTime OrderDateTime
        {
            get => _orderDateTime.AsUtc();
            set => _orderDateTime = value.Truncate();
        }

        DateTime _deliveryDateTime;
        public DateTime DeliveryDateTime
        {
            get => _deliveryDateTime.AsUtc();
            set => _deliveryDateTime = value.Truncate();
        }

        DateTime _expectedMinDeliveryDateTime;
        public DateTime ExpectedMinDeliveryDateTime
        {
            get => _expectedMinDeliveryDateTime.AsUtc();
            set => _expectedMinDeliveryDateTime = value.Truncate();
        }

        DateTime _expectedMaxDeliveryDateTime;
        public DateTime ExpectedMaxDeliveryDateTime
        {
            get => _expectedMaxDeliveryDateTime.AsUtc();
            set => _expectedMaxDeliveryDateTime = value.Truncate();
        }

        /// <summary>
        /// Total Price of the Line items
        /// </summary>
        public double SubTotal { get; set; }
        /// <summary>
        /// Sub Total + Service Fee + Delivery Fee
        /// </summary>        
        public double ServiceFee { get; set; }
        public double DeliveryFee { get; set; }
        public double Total { get; set; }
        /// <summary>
        /// The total discount of the Order based on the promo code or store discount.
        /// </summary>
        public double Discount { get; set; }
        /// <summary>
        /// The grand total of the order to be paid by the customer.
        /// </summary>
        public double GrandTotal { get; set; }                
        public double AmountPaid { get; set; }

        
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<OrderNoteBase> OrderNotes { get; set; } = new List<OrderNoteBase>();
        public virtual ICollection<OrderPaymentBase> OrderPayments { get; set; } = new List<OrderPaymentBase>();
        public virtual ICollection<OrderServiceFeeBase> OrderServiceFees { get; set; } = new List<OrderServiceFeeBase>();
        public virtual ICollection<OrderStatusHistoryBase> OrderStatusHistories { get; set; } = new List<OrderStatusHistoryBase>();

        public virtual ICollection<OrderLineItemBase> OrderLineItems { get; set; } = new List<OrderLineItemBase>();
    }

    public static class OrderExtension
    {
        public static void ThrowIfNull(this OrderBase me)
        {
            if (me == null)
                throw new ApplicationException("Order not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this OrderBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Order already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    public class OrderBaseConfiguration : EntityBaseConfiguration<OrderBase>
    {
        public override void Configure(EntityTypeBuilder<OrderBase> b)
        {
            b.ToTable("Order");
            b.HasKey(e => e.OrderId);

            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.CustomerId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.StoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ShippingSettingId).HasMaxLength(KeyMaxLength).IsRequired();

            b.Property(e => e.Number).HasMaxLength(NameMaxLength).IsRequired();

            b.Property(e => e.OrderDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.DeliveryDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ExpectedMinDeliveryDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ExpectedMaxDeliveryDateTime).HasMaxLength(KeyMaxLength).IsRequired();

            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();


            b.HasMany(e => e.OrderLineItems)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId);

            b.HasMany(e => e.OrderNotes)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId);

            b.HasMany(e => e.OrderPayments)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId);

            b.HasMany(e => e.OrderServiceFees)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(e => e.OrderStatusHistories)
                .WithOne(d => d.Order)
                .HasForeignKey(d => d.OrderId);
        }
    }
}

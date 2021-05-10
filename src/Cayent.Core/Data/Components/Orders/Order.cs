using System;
using System.Collections.Generic;
using Data.Enums;
using Data.Components.Customers;
using Data.Components.Settings;

using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Data.Components.Orders.OrderLineItems;
using Data.Components.BranchStores;
using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Components;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Components.Orders
{
    public abstract class OrderBase
    {
        [System.ComponentModel.DataAnnotations.Key]
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public virtual CustomerBase Customer { get; set; }

        public string BranchStoreId { get; set; }
        public virtual BranchStoreBase BranchStore { get; set; }

        public string ShippingSettingId { get; set; }
        public virtual ShippingSettingBase ShippingSetting { get; set; }

        public virtual OrderDeliveryAddressBase DeliveryAddress { get; set; }

        public string Number { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }

        public EnumPaymentMethod PaymentMethod { get; set; }

        [NotMapped]
        public bool StatusModified { get; protected set; } = false;
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

        public double GrossPrice { get; set; }
        public double ServiceFee { get; set; }
        public double DeliveryFee { get; set; }
        public double Deduction { get; set; }
        public double NetPrice { get; set; }
        public double AmountPaid { get; set; }

        //public virtual ICollection<OrderLineItemBase> LineItems { get; set; } = new List<OrderLineItemBase>();
        //public virtual ICollection<OrderNoteBase> OrderNotes { get; set; } = new List<OrderNoteBase>();
        //public virtual ICollection<OrderPaymentBase> OrderPayments { get; set; } = new List<OrderPaymentBase>();
        //public virtual ICollection<OrderServiceFeeBase> ServiceFees { get; set; } = new List<OrderServiceFeeBase>();
        //public virtual ICollection<OrderStatusHistoryBase> StatusHistories { get; set; } = new List<OrderStatusHistoryBase>();
        
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
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
            b.Property(e => e.BranchStoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ShippingSettingId).HasMaxLength(KeyMaxLength).IsRequired();

            b.Property(e => e.Number).HasMaxLength(NameMaxLength).IsRequired();

            b.Property(e => e.OrderDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.DeliveryDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ExpectedMinDeliveryDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ExpectedMaxDeliveryDateTime).HasMaxLength(KeyMaxLength).IsRequired();

            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            //b.OwnsOne(e => e.DeliveryAddress, da =>
            //{
            //    da.ToTable("OrderDeliveryAddress");
            //    da.HasKey(e => e.OrderId);

            //    da.WithOwner(e => e.Order);

            //    //b.Property(e => e.OrderAddressId).HasMaxLength(KeyMaxLength).IsRequired();
            //    da.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            //    da.Property(e => e.RecipientName).HasMaxLength(NameMaxLength).IsRequired();
            //    da.Property(e => e.PhoneNumber).HasMaxLength(NameMaxLength).IsRequired();
            //    da.Property(e => e.Address).HasMaxLength(DescMaxLength).IsRequired();
            //});

        }
    }
}

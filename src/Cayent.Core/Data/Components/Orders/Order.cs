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

namespace Data.Components.Orders
{
    public class Order
    {
        public string OrderId { get; set; }

        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public string BranchStoreId { get; set; }
        public virtual BranchStore BranchStore { get; set; }

        public string ShippingSettingId { get; set; }
        public virtual ShippingSetting ShippingSetting { get; set; }

        public virtual OrderDeliveryAddress DeliveryAddress { get; set; }

        public string Number { get; set; }
        public EnumOrderStatus OrderStatus { get; set; }

        public EnumPaymentMethod PaymentMethod { get; set; }

        [NotMapped]
        public bool StatusModified { get; private set; } = false;
        public void UpdateStatus(EnumOrderStatus status, string userId, string note)
        {
            StatusModified = true;

            if (OrderStatus == EnumOrderStatus.Completed || OrderStatus == EnumOrderStatus.Cancelled)
                throw new ApplicationException($"Order is already {OrderStatus}");

            OrderStatus = status;

            var now = DateTime.UtcNow;

            var itemsForDelete = new List<OrderStatusHistory>();
            switch (status)
            {
                case EnumOrderStatus.Placed:
                    // accepted + processed + delivered
                    var items1 = StatusHistories.Where(e => e.OrderStatus == EnumOrderStatus.Accepted
                                                            || e.OrderStatus == EnumOrderStatus.Processed
                                                            || e.OrderStatus == EnumOrderStatus.Picked
                                                            || e.OrderStatus == EnumOrderStatus.Delivered
                                                            );
                    itemsForDelete.AddRange(items1);
                    break;

                case EnumOrderStatus.Accepted:
                    // processed + delivered
                    var items2 = StatusHistories.Where(e => e.OrderStatus == EnumOrderStatus.Processed
                                                            || e.OrderStatus == EnumOrderStatus.Picked
                                                            || e.OrderStatus == EnumOrderStatus.Delivered);
                    itemsForDelete.AddRange(items2);
                    break;

                case EnumOrderStatus.Processed:
                    //  delete delivered
                    var items3 = StatusHistories.Where(e => e.OrderStatus == EnumOrderStatus.Picked
                                                            || e.OrderStatus == EnumOrderStatus.Delivered);
                    itemsForDelete.AddRange(items3);
                    break;

            }

            if (itemsForDelete.Any())
            {
                foreach (var item in itemsForDelete)
                {
                    StatusHistories.Remove(item);
                }
            }

            var history = StatusHistories.OrderBy(e => e.HistoryDateTime).FirstOrDefault(e => e.OrderStatus == OrderStatus);

            if (history == null)
            {
                history = new OrderStatusHistory
                {
                    OrderId = OrderId,
                    OrderStatus = OrderStatus,
                    HistoryDate = now.Date,
                    HistoryDateTime = now,
                    UserId = userId,
                    Note = note
                };

                StatusHistories.Add(history);
            }
            else
            {
                history.HistoryDate = now.Date;
                history.HistoryDateTime = now;
                history.UserId = userId;
                history.Note = note;
            }
        }

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

        public virtual ICollection<OrderLineItem> LineItems { get; set; } = new List<OrderLineItem>();
        public virtual ICollection<OrderNote> OrderNotes { get; set; } = new List<OrderNote>();
        public virtual ICollection<OrderPayment> OrderPayments { get; set; } = new List<OrderPayment>();
        public virtual ICollection<OrderServiceFee> ServiceFees { get; set; } = new List<OrderServiceFee>();
        public virtual ICollection<OrderStatusHistory> StatusHistories { get; set; } = new List<OrderStatusHistory>();
        //public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }

    public static class OrderExtension
    {
        public static void ThrowIfNull(this Order me)
        {
            if (me == null)
                throw new ApplicationException("Order not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this Order me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Order already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }
}

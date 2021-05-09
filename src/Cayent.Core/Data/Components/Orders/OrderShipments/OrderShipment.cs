using System;
using System.Collections.Generic;
using Cayent.Core.Common.Extensions;

namespace Data.Components.Orders.OrderShipments
{
    public class OrderShipment
    {
        public string OrderShipmentId { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

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
       
        public virtual ICollection<OrderShipmentLineItemShipment> LineItems { get; set; }
    }
}

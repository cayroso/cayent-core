using System.Collections.Generic;
using Data.Components.Products;

namespace Data.Components.Orders.OrderLineItems
{
    public class OrderLineItem
    {
        public string OrderLineItemId { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ProductPriceId { get; set; }
        public virtual ProductPrice ProductPrice { get; set; }
        
        public bool ProductOnSale { get; set; }
        public double Tax { get; set; }
        
        public string LineNumber { get; set; }
        public double ExtendedPrice { get; set; }

        public uint QuantityOrdered { get; set; }
        public uint QuantityPending { get; set; }
        public uint QuantityShipped { get; set; }
        public uint QuantityDelivered { get; set; }
        public uint QuantityReturned { get; set; }
        public uint QuantityCancelled { get; set; }
        public uint QuantityUndeliverable { get; set; }
        public uint QuantityReadyForPickup { get; set; }
    }
}

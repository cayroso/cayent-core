using Data.Components.Orders.OrderLineItems;

namespace Data.Components.Orders.OrderShipments
{
    public class OrderShipmentLineItemShipment
    {
        public string OrderShipmentId { get; set; }
        public virtual OrderShipment OrderShipment { get; set; }

        public string OrderLineItemId { get; set; }
        public virtual OrderLineItem OrderLineItem { get; set; }

        public uint Quantity { get; set; }
    }
}

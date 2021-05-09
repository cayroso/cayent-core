namespace Data.Components.Orders
{
    public class OrderDeliveryAddress
    {
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string RecipientName { get; set; }
        public string PhoneNumber { get; set; }

        public string Address { get; set; }
        public double GeoX { get; set; }
        public double GeoY { get; set; }
    }
}

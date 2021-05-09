using Data.Components.Settings;

namespace Data.Components.Orders
{
    public class OrderServiceFee
    {
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string ServiceFeeId { get; set; }
        public virtual ServiceFee ServiceFee { get; set; }
    }
}

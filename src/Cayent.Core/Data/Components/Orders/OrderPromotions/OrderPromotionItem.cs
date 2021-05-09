using Data.Components.Orders.OrderLineItems;

namespace Data.Components.Orders.OrderPromotions
{
    public class OrderPromotionItem
    {
        public string OrderPromotionId { get; set; }
        public virtual OrderPromotion OrderPromotion { get; set; }

        public string OrderLineItemId { get; set; }
        public virtual OrderLineItem OrderLineItem { get; set; }

        public double Duantity { get; set; }
    }
}

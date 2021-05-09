using System.Collections.Generic;
using Data.Components.Promotions;

namespace Data.Components.Orders.OrderPromotions
{
    public class OrderPromotion
    {
        public string OrderPromotionId { get; set; }

        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        public string PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }

        public virtual ICollection<OrderPromotionItem> PromotionItems { get; set; }
    }
}

using Data.Components.ItemGroups;
using Data.Components.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Components.Promotions
{
    public class PromotionProductFilter
    {
        public string PromotionProductFilterId { get; set; }

        public string PromotionId { get; set; }
        public virtual Promotion Promotion { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ItemGroupId { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }

    }
}

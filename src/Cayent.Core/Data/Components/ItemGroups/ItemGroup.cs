using Data.Components.Products;
using Data.Components.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Components.ItemGroups
{
    public class ItemGroup
    {
        public string ItemGroupId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<PromotionProductFilter> PromotionFilters { get; set; } = new List<PromotionProductFilter>();

    }
}

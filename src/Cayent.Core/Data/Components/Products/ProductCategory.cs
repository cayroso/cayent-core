using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Components.Products
{
    public class ProductCategory
    {
        public string ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public virtual ProductCategory Parent { get; set; }
        public virtual ICollection<ProductCategory> Children { get; set; } = new List<ProductCategory>();

        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cayent.Core.Common.Extensions;
using Data.Components.Orders.OrderLineItems;

namespace Data.Components.Products
{
    public class ProductPrice
    {
        public string ProductPriceId { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public double Cogs { get; set; }
        public double Price { get; set; }
        public double SalePrice { get; set; }

        DateTime _saleStart = DateTime.MaxValue;
        public DateTime SaleStart
        {
            get => _saleStart.AsUtc();
            set => _saleStart = value.Truncate();
        }

        DateTime _saleEnd = DateTime.MaxValue;
        public DateTime SaleEnd
        {
            get => _saleEnd.AsUtc();
            set => _saleEnd = value.Truncate();
        }

        public uint LoyaltyPoints { get; set; }

        public bool Active { get; set; } = true;

        public virtual ICollection<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();
    }
}

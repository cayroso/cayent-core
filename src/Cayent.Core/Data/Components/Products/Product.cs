using Data.Components.BranchStores;
using Data.Components.ItemGroups;
using Data.Components.Orders.OrderLineItems;
using Data.Components.Promotions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Enums;

namespace Data.Components.Products
{
    public class Product
    {
        public string ProductId { get; set; }

        public string ProductCategoryId { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string PrimaryImageUrl { get; set; }

        #region Detail

        public string ItemGroupId { get; set; }
        public virtual ItemGroup ItemGroup { get; set; }

        public string Highlight { get; set; }

        public EnumProductAvailability Availability { get; set; }

        public
        DateTime _availabilityDateTime = DateTime.MaxValue;
        public DateTime AvailabilityDateTime
        {
            get => _availabilityDateTime.AsUtc();
            set => _availabilityDateTime = value.Truncate();
        }

        DateTime _expirationDateTime = DateTime.MaxValue;
        public DateTime ExpirationDateTime
        {
            get => _expirationDateTime.AsUtc();
            set => _expirationDateTime = value.Truncate();
        }

        public uint MultiPack { get; set; } = 1;

        #endregion

        #region Inventory

        public double Stock { get; set; }
        public double SafetyStock { get; set; }
        public double ReorderLevel { get; set; }

        #endregion

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<BranchStoreProduct> BranchStoreProducts { get; set; } = new List<BranchStoreProduct>();
        public virtual ICollection<ProductImage> Images { get; set; } = new List<ProductImage>();
        public virtual ICollection<ProductPrice> Prices { get; set; } = new List<ProductPrice>();

        public virtual ICollection<OrderLineItem> OrderLineItems { get; set; } = new List<OrderLineItem>();
        public virtual ICollection<PromotionProductFilter> PromotionFilters { get; set; } = new List<PromotionProductFilter>();
    }

    public static class ProductExtension
    {
        public static void ThrowIfNull(this Product me)
        {
            if (me == null)
                throw new ApplicationException("Product not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this Product me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Product already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }
}

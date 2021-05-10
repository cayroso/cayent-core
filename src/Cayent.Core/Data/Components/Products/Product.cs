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
using Cayent.Core.Data.Components;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Data.Components.Products
{
    public abstract class ProductBase
    {
        public string ProductId { get; set; }

        public string ProductCategoryId { get; set; }
        public virtual ProductCategoryBase ProductCategory { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public string PrimaryImageUrl { get; set; }

        #region Detail

        public string ItemGroupId { get; set; }
        public virtual ItemGroupBase ItemGroup { get; set; }

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

        //public virtual ICollection<BranchStoreProductBase> BranchStoreProducts { get; set; } = new List<BranchStoreProductBase>();
        //public virtual ICollection<ProductImageBase> Images { get; set; } = new List<ProductImageBase>();
        //public virtual ICollection<ProductPriceBase> Prices { get; set; } = new List<ProductPriceBase>();

        //public virtual ICollection<OrderLineItemBase> OrderLineItems { get; set; } = new List<OrderLineItemBase>();
        //public virtual ICollection<PromotionProductFilterBase> PromotionFilters { get; set; } = new List<PromotionProductFilterBase>();
    }

    public static class ProductExtension
    {
        public static void ThrowIfNull(this ProductBase me)
        {
            if (me == null)
                throw new ApplicationException("Product not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this ProductBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Product already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    public class ProductBaseConfiguration : EntityBaseConfiguration<ProductBase>
    {
        public override void Configure(EntityTypeBuilder<ProductBase> b)
        {
            b.ToTable("Product");
            b.HasKey(e => e.ProductId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductCategoryId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.Description).HasMaxLength(DescMaxLength);
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);
        }
    }
}

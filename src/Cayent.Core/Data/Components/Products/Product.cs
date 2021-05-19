
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cayent.Core.Data.Components.Items;
using Cayent.Core.Data.Components.Stores;
using Cayent.Core.Data.Components.Orders.OrderLineItems;
using Cayent.Core.Data.Components.Promotions;

namespace Cayent.Core.Data.Components.Products
{
    public class ProductBase
    {
        public string ProductId { get; set; }

        public string ItemId { get; set; }
        public virtual ItemBase Item { get; set; }

        public string ProductGroupId { get; set; }
        public virtual ProductGroupBase ProductGroup { get; set; }

        public string Sku { get; set; }
        public string Summary { get; set; }
        public string PrimaryImageUrl { get; set; }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<ProductCategoryBase> ProductCategories { get; set; } = new List<ProductCategoryBase>();
        public virtual ICollection<ProductImageBase> ProductImages { get; set; } = new List<ProductImageBase>();
        public virtual ICollection<ProductPriceBase> ProductPrices { get; set; } = new List<ProductPriceBase>();
        
        public virtual ICollection<OrderLineItemBase> OrderLineItems { get; set; } = new List<OrderLineItemBase>();
        public virtual ICollection<PromotionProductFilterBase> PromotionProductFilters { get; set; } = new List<PromotionProductFilterBase>();
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
            b.HasIndex(e => e.Sku).IsUnique();

            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ItemId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Sku).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasOne(e => e.Item)
                .WithOne()
                .HasForeignKey<ProductBase>(fk => fk.ProductId);


            b.HasMany(e => e.ProductCategories)
                .WithOne(d => d.Product)
                .HasForeignKey(fk => fk.ProductId);

            b.HasMany(e => e.ProductImages)
                .WithOne(d => d.Product)
                .HasForeignKey(fk => fk.ProductId);

            b.HasMany(e => e.ProductPrices)
                .WithOne(d => d.Product)
                .HasForeignKey(fk => fk.ProductId);

            //b.HasMany(e => e.ProductVariants)
            //    .WithOne(d => d.Product)
            //    .HasForeignKey(fk => fk.ProductId);

            b.HasMany(e => e.OrderLineItems)
                .WithOne(d => d.Product)
                .HasForeignKey(fk => fk.ProductId);

            b.HasMany(e => e.PromotionProductFilters)
                .WithOne(d => d.Product)
                .HasForeignKey(fk => fk.ProductId);

            b.HasQueryFilter(e => e.Active);
        }
    }
}

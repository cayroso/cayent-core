using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Cayent.Core.Data.Components.Categories;
using System.Collections.Generic;

namespace Cayent.Core.Data.Components.Products
{
    public class ProductCategoryBase
    {
        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }
        public string CategoryId { get; set; }
        public virtual CategoryBase Category { get; set; }        
    }

    public class ProductCategoryBaseConfiguration : EntityBaseConfiguration<ProductCategoryBase>
    {
        public override void Configure(EntityTypeBuilder<ProductCategoryBase> b)
        {
            b.ToTable("ProductCategory");
            b.HasKey(e => new { e.ProductId, e.CategoryId });

            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.CategoryId).HasMaxLength(KeyMaxLength);            
        }
    }
}

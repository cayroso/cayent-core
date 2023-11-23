using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Products
{
    internal abstract class ProductCategoryBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductCategoryId { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public virtual ProductCategoryBase Parent { get; set; }
        //public virtual ICollection<ProductCategoryBase> Children { get; set; } = new List<ProductCategoryBase>();

        //public virtual ICollection<ProductBase> Products { get; set; } = new List<ProductBase>();

    }

    internal class ProductCategoryBaseConfiguration : EntityBaseConfiguration<ProductCategoryBase>
    {
        public override void Configure(EntityTypeBuilder<ProductCategoryBase> b)
        {
            b.ToTable("ProductCategory");
            b.HasKey(e => e.ProductCategoryId);

            b.Property(e => e.ProductCategoryId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ParentId).HasMaxLength(KeyMaxLength);
        }
    }
}

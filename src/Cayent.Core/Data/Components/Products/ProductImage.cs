using Cayent.Core.Data.Fileuploads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Products
{
    public class ProductImageBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductImageId { get; set; }

        public string ProductId { get; set; }

        public virtual ProductBase Product { get; set; }

        public string ImageId { get; set; }
        public virtual FileUpload Image { get; set; }

        public uint SortOrder { get; set; }
    }

    public class ProductImageBaseConfiguration : EntityBaseConfiguration<ProductImageBase>
    {
        public override void Configure(EntityTypeBuilder<ProductImageBase> b)
        {
            b.ToTable("ProductImage");
            b.HasKey(e => e.ProductImageId);
            b.HasIndex(e => new { e.ProductId, e.ImageId });

            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ImageId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Product.Active);
        }
    }
}

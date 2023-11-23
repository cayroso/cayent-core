using System.ComponentModel.DataAnnotations.Schema;
using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Products
{
    internal abstract class ProductPriceBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ProductPriceId { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }

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

        //public virtual ICollection<OrderLineItemBase> OrderLineItems { get; set; } = new List<OrderLineItemBase>();
    }

    internal class ProductPriceBaseConfiguration : EntityBaseConfiguration<ProductPriceBase>
    {
        public override void Configure(EntityTypeBuilder<ProductPriceBase> b)
        {
            b.ToTable("ProductPrice");
            b.HasKey(e => e.ProductPriceId);

            b.Property(e => e.ProductPriceId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);
        }
    }
}

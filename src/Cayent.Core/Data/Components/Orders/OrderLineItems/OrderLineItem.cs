using Cayent.Core.Data.Components.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Orders.OrderLineItems
{
    internal class OrderLineItemBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderLineItemId { get; set; }

        public string OrderId { get; set; }
        public virtual OrderBase Order { get; set; }

        public string ProductId { get; set; }
        public virtual ProductBase Product { get; set; }

        public string ProductPriceId { get; set; }
        public virtual ProductPriceBase ProductPrice { get; set; }

        public bool ProductOnSale { get; set; }

        public string LineNumber { get; set; }
        public double ExtendedPrice { get; set; }

        public uint QuantityOrdered { get; set; }        
    }

    internal class OrderLineItemBaseConfiguration : EntityBaseConfiguration<OrderLineItemBase>
    {
        public override void Configure(EntityTypeBuilder<OrderLineItemBase> b)
        {
            b.ToTable("OrderLineItem");
            b.HasKey(e => e.OrderLineItemId);

            b.Property(e => e.OrderLineItemId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductPriceId).HasMaxLength(KeyMaxLength).IsRequired();

            //b.HasQueryFilter(e => e.Product.Active);
        }
    }
}

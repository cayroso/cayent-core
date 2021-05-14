using Cayent.Core.Data.Components.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Orders.OrderLineItems
{
    public abstract class OrderLineItemBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderLineItemId { get; set; }

        public string OrderId { get; set; }
        public OrderBase Order { get; set; }

        public string ProductId { get; set; }
        public ProductBase Product { get; set; }

        public string ProductPriceId { get; set; }
        public ProductPriceBase ProductPrice { get; set; }

        public bool ProductOnSale { get; set; }
        public double Tax { get; set; }

        public string LineNumber { get; set; }
        public double ExtendedPrice { get; set; }

        public uint QuantityOrdered { get; set; }
        public uint QuantityPending { get; set; }
        public uint QuantityShipped { get; set; }
        public uint QuantityDelivered { get; set; }
        public uint QuantityReturned { get; set; }
        public uint QuantityCancelled { get; set; }
        public uint QuantityUndeliverable { get; set; }
        public uint QuantityReadyForPickup { get; set; }
    }

    public class OrderLineItemBaseConfiguration : EntityBaseConfiguration<OrderLineItemBase>
    {
        public override void Configure(EntityTypeBuilder<OrderLineItemBase> b)
        {
            b.ToTable("OrderLineItem");
            b.HasKey(e => e.OrderLineItemId);

            b.Property(e => e.OrderLineItemId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ProductPriceId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Product.Active);
        }
    }
}

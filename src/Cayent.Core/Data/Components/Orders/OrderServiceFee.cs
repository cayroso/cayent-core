using Cayent.Core.Data.Components.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Orders
{
    public abstract class OrderServiceFeeBase
    {        
        public string OrderId { get; set; }
        public OrderBase Order { get; set; }

        public string ServiceFeeId { get; set; }
        public ServiceFeeBase ServiceFee { get; set; }
    }

    public class OrderServiceFeeBaseConfiguration : EntityBaseConfiguration<OrderServiceFeeBase>
    {
        public override void Configure(EntityTypeBuilder<OrderServiceFeeBase> b)
        {
            b.ToTable("OrderServiceFee");
            b.HasKey(e => new { e.OrderId, e.ServiceFeeId });

            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ServiceFeeId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.ServiceFee.Active);
        }
    }
}

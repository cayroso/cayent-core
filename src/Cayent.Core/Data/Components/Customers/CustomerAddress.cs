using Cayent.Core.Data.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Customers
{
    internal abstract class CustomerAddressBase
    {        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerAddressId { get; set; }

        public string CustomerId { get; set; }
        public virtual CustomerBase Customer { get; set; }
        public EnumAddressType AddressType { get; set; }
        public bool IsPrimary { get; set; }
        public string Address { get; set; }
        public double GeoX { get; set; }
        public double GeoY { get; set; }
    }

    internal class CustomerAddressBaseConfiguration : EntityBaseConfiguration<CustomerAddressBase>
    {
        public override void Configure(EntityTypeBuilder<CustomerAddressBase> b)
        {
            b.ToTable("CustomerAddress");
            b.HasKey(e => e.CustomerAddressId);

            b.Property(e => e.CustomerId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Address).HasMaxLength(DescMaxLength);

            b.HasQueryFilter(e => e.Customer.Active);
        }
    }
}

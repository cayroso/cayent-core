using System;
using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Cayent.Core.Data.Components.Orders;

namespace Cayent.Core.Data.Components.Customers
{
    internal class CustomerBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        [NotMapped]
        public string FirstLastName => $"{FirstName} {LastName}";

        public string PhoneNumber { get; set; }
        public string Email { get; set; }

        DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<CustomerAddressBase> CustomerAddresses { get; set; } = new List<CustomerAddressBase>();
        
    }

    internal class CustomerBaseConfiguration : EntityBaseConfiguration<CustomerBase>
    {
        public override void Configure(EntityTypeBuilder<CustomerBase> b)
        {
            b.ToTable("Customer");
            b.HasKey(e => e.CustomerId);

            b.Property(e => e.CustomerId).HasMaxLength(KeyMaxLength).IsRequired();

            b.Property(e => e.FirstName).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.LastName).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.PhoneNumber).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);

            b.HasMany(e => e.CustomerAddresses)
                .WithOne(d => d.Customer)
                .HasForeignKey(fk => fk.CustomerId);

            
        }
    }
}

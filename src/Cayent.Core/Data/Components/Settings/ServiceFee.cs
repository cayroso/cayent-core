using System;
using System.Collections.Generic;
using Cayent.Core.Data.Components;
using Cayent.Core.Data.Components.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Settings
{
    internal abstract class ServiceFeeBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ServiceFeeId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public bool Active { get; set; } = true;

        //public virtual ICollection<OrderServiceFeeBase> OrderServiceFees { get; set; } = new List<OrderServiceFeeBase>();

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }
    internal static class ServiceFeeExtension
    {
        public static void ThrowIfNull(this ServiceFeeBase me)
        {
            if (me == null)
                throw new ApplicationException("ServiceFee not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this ServiceFeeBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("ServiceFee already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    internal class ServiceFeeBaseConfiguration : EntityBaseConfiguration<ServiceFeeBase>
    {
        public override void Configure(EntityTypeBuilder<ServiceFeeBase> b)
        {
            b.ToTable("ServiceFee");
            b.HasKey(e => e.ServiceFeeId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ServiceFeeId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);
        }
    }
}

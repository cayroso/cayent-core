
using Cayent.Core.Data.Components;
using Cayent.Core.Data.Components.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.BranchStores
{
    internal abstract class BranchStoreBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BranchStoreId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double GeoX { get; set; }
        public double GeoY { get; set; }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        //public virtual ICollection<OrderBase> Orders { get; set; } = new List<OrderBase>();
        //public virtual ICollection<BranchStoreProductBase> BranchStoreProducts { get; set; } = new List<BranchStoreProductBase>();
        //public virtual ICollection<BranchStoreUserBase> BranchUsers { get; set; } = new List<BranchStoreUserBase>();
        //public virtual ICollection<Bottle> Bottles { get; set; } = new List<Bottle>();
    }

    internal static class BranchStoreExtension
    {
        public static void ThrowIfNull(this BranchStoreBase me)
        {
            if (me == null)
                throw new ApplicationException("Branch Store not found.");
        }
        public static void ThrowIfNullOrAlreadyUpdated(this BranchStoreBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Branch Store already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    internal class BranchStoreBaseConfiguration : EntityBaseConfiguration<BranchStoreBase>
    {
        public override void Configure(EntityTypeBuilder<BranchStoreBase> b)
        {
            b.ToTable("BranchStore");
            b.HasKey(e => e.BranchStoreId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.BranchStoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);

        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Stores
{
    internal class StoreBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StoreId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double GeoX { get; set; }
        public double GeoY { get; set; }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<StoreProductBase> StoreProducts { get; set; } = new List<StoreProductBase>();

        //public virtual ICollection<StoreStockBase> StoreStocks { get; set; } = new List<StoreStockBase>();

        public virtual ICollection<StoreUserBase> StoreUsers { get; set; } = new List<StoreUserBase>();


    }

    internal static class StoreBaseExtension
    {
        public static void ThrowIfNull(this StoreBase me)
        {
            if (me == null)
                throw new ApplicationException("Branch Store not found.");
        }
        public static void ThrowIfNullOrAlreadyUpdated(this StoreBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Branch Store already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    internal class StoreBaseConfiguration : EntityBaseConfiguration<StoreBase>
    {
        public override void Configure(EntityTypeBuilder<StoreBase> b)
        {
            b.ToTable("Store");
            b.HasKey(e => e.StoreId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.StoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);


            b.HasMany(e => e.StoreProducts)
                .WithOne(d => d.Store)
                .HasForeignKey(d => d.StoreId)
                ;

            //b.HasMany(e => e.StoreStocks)
            //    .WithOne(d => d.Store)
            //    .HasForeignKey(d => d.StoreId);

            b.HasMany(e => e.StoreUsers)
                .WithOne(d => d.Store)
                .HasForeignKey(d => d.StoreId);


        }
    }
}

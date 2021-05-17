using System;
using Data.Enums;
using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Cayent.Core.Data.Components.Orders;

namespace Cayent.Core.Data.Components.Settings
{
    public class ShippingSettingBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ShippingSettingId { get; set; }
        public string Name { get; set; }

        public uint MinDelayInHours { get; set; }
        public uint MaxDelayInHours { get; set; }

        DateTime _cutOff;
        public DateTime CutOff
        {
            get => _cutOff.AsUtc();
            set => _cutOff = value.Truncate();
        }

        public double MinimumOrderValue { get; set; }
        public double FlatRate { get; set; }
        public double PricePercentage { get; set; }
        public EnumShipmentType ShipmentType { get; set; }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }

    public static class ShippingSettingExtension
    {
        public static void ThrowIfNull(this ShippingSettingBase me)
        {
            if (me == null)
                throw new ApplicationException("Shipping Setting not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this ShippingSettingBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Shipping Setting already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    public class ShippingSettingBaseConfiguration : EntityBaseConfiguration<ShippingSettingBase>
    {
        public override void Configure(EntityTypeBuilder<ShippingSettingBase> b)
        {
            b.ToTable("ShippingSetting");
            b.HasKey(e => e.ShippingSettingId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ShippingSettingId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);
        }
    }
}

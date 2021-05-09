using System;
using System.Collections.Generic;
using Data.Components.Orders;
using Data.Enums;
using Cayent.Core.Common.Extensions;

namespace Data.Components.Settings
{
    public class ShippingSetting
    {
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

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }

    public static class ShippingSettingExtension
    {
        public static void ThrowIfNull(this ShippingSetting me)
        {
            if (me == null)
                throw new ApplicationException("Shipping Setting not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this ShippingSetting me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Shipping Setting already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }
}

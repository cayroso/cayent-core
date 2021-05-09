using System;
using System.Collections.Generic;
using Data.Components.Orders;

namespace Data.Components.Settings
{
    public class ServiceFee
    {
        public string ServiceFeeId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public double Amount { get; set; }
        public bool Active { get; set; } = true;

        public virtual ICollection<OrderServiceFee> OrderServiceFees { get; set; } = new List<OrderServiceFee>();

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }
    public static class ServiceFeeExtension
    {
        public static void ThrowIfNull(this ServiceFee me)
        {
            if (me == null)
                throw new ApplicationException("ServiceFee not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this ServiceFee me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("ServiceFee already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }
}

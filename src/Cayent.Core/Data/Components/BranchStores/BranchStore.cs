
using Data.Components.Orders;
using System;
using System.Collections.Generic;

namespace Data.Components.BranchStores
{
    public class BranchStore
    {
        public string BranchStoreId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double GeoX { get; set; }
        public double GeoY { get; set; }

        public bool Active { get; set; } = true;
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
        public virtual ICollection<BranchStoreProduct> BranchStoreProducts { get; set; } = new List<BranchStoreProduct>();
        public virtual ICollection<BranchStoreUser> BranchUsers { get; set; } = new List<BranchStoreUser>();
        //public virtual ICollection<Bottle> Bottles { get; set; } = new List<Bottle>();
    }

    public static class BranchStoreExtension
    {
        public static void ThrowIfNull(this BranchStore me)
        {
            if (me == null)
                throw new ApplicationException("Branch Store not found.");
        }
        public static void ThrowIfNullOrAlreadyUpdated(this BranchStore me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Branch Store already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }
}

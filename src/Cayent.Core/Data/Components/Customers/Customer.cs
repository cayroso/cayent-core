using System;
using System.Collections.Generic;
using Data.Components.Orders;
using Cayent.Core.Common.Extensions;

namespace Data.Components.Customers
{
    public class Customer
    {
        public string CustomerId { get; set; }

        public double LoyaltyPoints { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FirstLastName => $"{FirstName} {LastName}";

        public string PhoneNumber { get; set; }
        public string Notes { get; set; }

        public virtual ICollection<CustomerAddress> CustomerAddresses { get; set; } = new List<CustomerAddress>();


        DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public bool Active { get; set; } = true;

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }

    public static class CustomerExtension
    {
        public static void ThrowIfNull(this Customer me)
        {
            if (me == null)
                throw new ApplicationException("Customer not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this Customer me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("Customer already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }
}

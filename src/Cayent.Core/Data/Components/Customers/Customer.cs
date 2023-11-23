using Cayent.Core.Common.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Customers
{
    internal abstract class CustomerBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerId { get; set; }

        public double LoyaltyPoints { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FirstLastName => $"{FirstName} {LastName}";

        public string PhoneNumber { get; set; }
        public string Notes { get; set; }

        //public virtual ICollection<CustomerAddressBase> CustomerAddresses { get; set; } = new List<CustomerAddressBase>();


        DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }

        //public virtual ICollection<OrderBase> Orders { get; set; } = new List<OrderBase>();

        public bool Active { get; set; } = true;

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }

    //public static class CustomerExtension
    //{
    //    public static void ThrowIfNull(this CustomerBase me)
    //    {
    //        if (me == null)
    //            throw new ApplicationException("Customer not found.");
    //    }

    //    public static void ThrowIfNullOrAlreadyUpdated(this CustomerBase me, string currentToken, string newToken)
    //    {
    //        me.ThrowIfNull();

    //        if (string.IsNullOrWhiteSpace(newToken))
    //            throw new ApplicationException("Anti-forgery token not found.");

    //        if (me.ConcurrencyToken != currentToken)
    //            throw new ApplicationException("Customer already updated by another user.");

    //        me.ConcurrencyToken = newToken;
    //    }
    //}

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
            //b.Property(e => e.Address).HasMaxLength(DescMaxLength);
            b.Property(e => e.Notes).HasMaxLength(NoteMaxLength);
            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Active);
        }
    }
}

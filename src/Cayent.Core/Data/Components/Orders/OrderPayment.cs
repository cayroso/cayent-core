using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Orders
{
    internal abstract class OrderPaymentBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderPaymentId { get; set; }

        public string OrderId { get; set; }
        public virtual OrderBase Order { get; set; }
        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public double AmountDue { get; set; }
        public double AmountPaid { get; set; }

        public string Note { get; set; }

        DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }
    }

    internal class OrderPaymentBaseConfiguration : EntityBaseConfiguration<OrderPaymentBase>
    {
        public override void Configure(EntityTypeBuilder<OrderPaymentBase> b)
        {
            b.ToTable("OrderPayment");
            b.HasKey(e => e.OrderPaymentId);

            b.Property(e => e.OrderPaymentId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Note).HasMaxLength(NoteMaxLength).IsRequired();
        }
    }
}

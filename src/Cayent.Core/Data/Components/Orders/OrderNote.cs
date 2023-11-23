using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Orders
{
    internal abstract class OrderNoteBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string OrderNoteId { get; set; }
        
        public string OrderId { get; set; }
        public virtual OrderBase Order { get; set; }
        
        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string Note { get; set; }
        public bool SystemGenerated { get; set; } = false;

        DateTime _dateCreated;
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }
    }

    internal class OrderNoteBaseConfiguration : EntityBaseConfiguration<OrderNoteBase>
    {
        public override void Configure(EntityTypeBuilder<OrderNoteBase> b)
        {
            b.ToTable("OrderNote");
            b.HasKey(e => e.OrderNoteId);

            b.Property(e => e.OrderNoteId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Note).HasMaxLength(NoteMaxLength).IsRequired();
        }
    }
}

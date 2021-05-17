using System;
using Data.Enums;
using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Cayent.Core.Data.Components.Orders
{
    public class OrderStatusHistoryBase
    {
        public string OrderId { get; set; }
        public virtual OrderBase Order { get; set; }

        public DateTime HistoryDate { get; set; }

        DateTime _historyDateTime;
        public DateTime HistoryDateTime
        {
            get => _historyDateTime.AsUtc();
            set => _historyDateTime = value.Truncate();
        }

        public EnumOrderStatus OrderStatus { get; set; }

        public string UserId { get; set; }
        public UserBase User { get; set; }

        public string Note { get; set; }
    }

    public class OrderStatusHistoryBaseConfiguration : EntityBaseConfiguration<OrderStatusHistoryBase>
    {
        public override void Configure(EntityTypeBuilder<OrderStatusHistoryBase> b)
        {
            b.ToTable("OrderStatusHistory");
            b.HasKey(e => new { e.OrderId, e.OrderStatus });

            b.Property(e => e.OrderId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.HistoryDate).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.HistoryDateTime).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Note).HasMaxLength(NoteMaxLength).IsRequired();
        }
    }
}

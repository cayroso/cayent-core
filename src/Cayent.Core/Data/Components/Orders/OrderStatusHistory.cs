using System;
using Data.Enums;
using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Users;

namespace Data.Components.Orders
{
    public class OrderStatusHistory
    {
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }

        public DateTime HistoryDate { get; set; }

        DateTime _historyDateTime;
        public DateTime HistoryDateTime
        {
            get => _historyDateTime.AsUtc();
            set => _historyDateTime = value.Truncate();
        }

        public EnumOrderStatus OrderStatus { get; set; }

        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string Note { get; set; }
    }
}

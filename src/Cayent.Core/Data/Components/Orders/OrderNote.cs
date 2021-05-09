using System;
using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Users;

namespace Data.Components.Orders
{
    public class OrderNote
    {
        public string OrderNoteId { get; set; }
        
        public string OrderId { get; set; }
        public virtual Order Order { get; set; }
        
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
}

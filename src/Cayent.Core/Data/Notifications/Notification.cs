
using Cayent.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cayent.Core.Data.Components.Users;

namespace Cayent.Core.Data.Notifications
{
    public enum EnumNotificationType
    {
        Unknown = 0,
        Primary,
        Secondary,
        Success,
        Info,
        Warning,
        Error
    }

    internal class Notification
    {
        public string NotificationId { get; set; }
        public EnumNotificationType NotificationType { get; set; }
        public string IconClass { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string ReferenceId { get; set; }

        DateTime _dateSent = DateTime.UtcNow.Truncate();
        public DateTime DateSent
        {
            get => _dateSent.AsUtc();
            set => _dateSent = value.Truncate();
        }

        public virtual ICollection<NotificationReceiver> Receivers { get; set; } = new List<NotificationReceiver>();

        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();
    }

    internal class NotificationReceiver
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string NotificationReceiverId { get; set; }

        public string NotificationId { get; set; }
        public virtual Notification Notification { get; set; }

        public string ReceiverId { get; set; }
        public virtual UserBase Receiver { get; set; }


        DateTime _dateReceived;
        public DateTime DateReceived
        {
            get => _dateReceived;
            set => _dateReceived = value.Truncate().AsUtc();
        }

        DateTime _dateRead;
        public DateTime DateRead
        {
            get => _dateRead;
            set => _dateRead = value.Truncate().AsUtc();
        }

        [NotMapped]
        public bool IsRead => DateTime.UtcNow > DateRead;
    }
}

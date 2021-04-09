using Cayent.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cayent.Core.Data.Common
{
    public class EventLog
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventLogId { get; set; }
        public int EventId { get; set; }
        public string LogLevel { get; set; }
        public string Message { get; set; }

        DateTime _createdTime = DateTime.UtcNow.Truncate();
        public DateTime CreatedTime
        {
            get => _createdTime.AsUtc();
            set => _createdTime = value.Truncate();
        }
    }
}

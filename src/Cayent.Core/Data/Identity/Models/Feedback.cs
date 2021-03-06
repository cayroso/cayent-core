﻿using Cayent.Core.Common.Extensions;
using Cayent.Core.Data.Enums;
using Cayent.Core.Data.Identity.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Identity.Models
{
    public class Feedback
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string FeedbackId { get; set; }

        public string UserId { get; set; }
        public IdentityWebUser User { get; set; }

        public EnumFeedbackCategory FeedbackCategory { get; set; }

        public int Rating { get; set; }
        public string Comment { get; set; }



        DateTime _dateCreated = DateTime.UtcNow.Truncate();
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }

        [ConcurrencyCheck]
        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
    }
}

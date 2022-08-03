//using Cayent.Core.Common.Extensions;
//using Cayent.Core.Data.Enums;
//using Data.Identity.Models.Users;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace Data.Identity.Models
//{
//    internal class Feedback
//    {
//        [DatabaseGenerated(DatabaseGeneratedOption.None)]
//        public string FeedbackId { get; set; }

//        public string UserId { get; set; }
//        public IdentityWebUser User { get; set; }

//        public EnumFeedbackCategory FeedbackCategory { get; set; }

//        public int Rating { get; set; }
//        public string Comment { get; set; }



//        DateTimeOffset _dateCreated = DateTimeOffset.UtcNow.Truncate();
//        public DateTimeOffset DateCreated
//        {
//            get => _dateCreated;
//            set => _dateCreated = value.Truncate();
//        }

//        [ConcurrencyCheck]
//        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
//    }

//    internal class FeedbackConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<Feedback>
//    {
//        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Feedback> b)
//        {
//            b.ToTable("Feedback");
//            b.HasKey(e => e.FeedbackId);

//            b.Property(e => e.FeedbackId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
//        }
//    }
//}

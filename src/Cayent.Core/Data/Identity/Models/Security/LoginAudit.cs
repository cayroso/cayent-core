
//using Cayent.Core.Common.Extensions;
//using Data.Identity.Models.Users;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace Data.Identity.Models
//{
//    internal class LoginAudit
//    {
//        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
//        public string LoginAuditId { get; set; }
//        public string UserId { get; set; }
//        public virtual IdentityWebUser User { get; set; }

//        public string RemoteIpAddress { get; set; }

//        DateTimeOffset _loginDate;
//        public DateTimeOffset LoginDate
//        {
//            get => _loginDate;
//            set => _loginDate = value.Truncate();
//        }
//    }

//    internal class LoginAuditConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<LoginAudit>
//    {
//        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<LoginAudit> b)
//        {
//            b.ToTable("LoginAudit");
//            b.HasKey(e => e.LoginAuditId);

//            b.Property(e => e.LoginAuditId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();

//            b.Property(e => e.RemoteIpAddress).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.LoginDate).HasMaxLength(KeyMaxLength).IsRequired();
//        }
//    }
//}

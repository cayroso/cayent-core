//using Data.Identity.Models.Security;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Data.Identity.Models.Users
//{
//    internal class IdentityWebUser : IdentityUser
//    {
//        public string TenantId { get; set; }
//        public virtual Tenant Tenant { get; set; }
//        public virtual UserInformation UserInformation { get; set; }
//        public virtual ICollection<LoginAudit> LoginAudits { get; set; }
//    }

//    internal class IdentityWebUserConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<IdentityWebUser>
//    {
//        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<IdentityWebUser> b)
//        {
//            b.ToTable("User");

//            b.Property(q => q.Id).HasColumnName("UserId").HasMaxLength(KeyMaxLength);

//            b.Property(e => e.UserName).HasMaxLength(NameMaxLength).IsRequired();
//            b.HasIndex(e => e.UserName).IsUnique();

//            b.Property(e => e.Email).HasMaxLength(NameMaxLength).IsRequired();
//            b.HasIndex(e => e.Email).IsUnique();

//            b.Property(e => e.TenantId).HasMaxLength(KeyMaxLength);

//            b.HasOne(e => e.UserInformation)
//                .WithOne(d => d.User)
//                .HasForeignKey<UserInformation>(d => d.UserId);

//            b.HasMany(e => e.LoginAudits)
//                .WithOne(d => d.User)
//                .HasForeignKey(d => d.UserId);

//        }
//    }
//}

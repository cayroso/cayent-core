//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Text;

//namespace Data.Identity.Models.Users
//{
//    internal class UserInformation
//    {
//        public string UserId { get; set; }
//        public virtual IdentityWebUser User { get; set; }
//        public string ImageId { get; set; }
//        //public virtual Image Image { get; set; }
//        public string FirstName { get; set; }
//        public string MiddleName { get; set; }
//        public string LastName { get; set; }
//        [NotMapped]
//        public string FirstLastName => $"{FirstName} {LastName}";

//        [NotMapped]
//        public string FullName
//        {
//            get
//            {
//                if (string.IsNullOrWhiteSpace(MiddleName))
//                {
//                    return $"{FirstName} {MiddleName} {LastName}";
//                }

//                return FirstLastName;
//            }
//        }

//        public string Theme { get; set; }

//        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
//    }

//    public static class UserInformationExtension
//    {
//        public static void ThrowIfNull(this UserInformation me)
//        {
//            if (me == null)
//                throw new ApplicationException("Not found.");
//        }

//        public static void ThrowIfNullOrAlreadyUpdated(this UserInformation me, string currentToken, string newToken)
//        {
//            me.ThrowIfNull();

//            if (string.IsNullOrWhiteSpace(newToken))
//                throw new ApplicationException("Anti-forgery token not found.");

//            if (me.ConcurrencyToken != currentToken)
//                throw new ApplicationException("Already updated by another user.");

//            me.ConcurrencyToken = newToken;
//        }
//    }

//    internal class UserInformationConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<UserInformation>
//    {
//        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserInformation> b)
//        {
//            b.ToTable("UserInformation");
//            b.HasKey(e => e.UserId);

//            //b.Property(e => e.UserInformationId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.ImageId).HasMaxLength(KeyMaxLength).IsRequired(false);
//            b.Property(e => e.FirstName).HasMaxLength(DescMaxLength).IsRequired();
//            b.Property(e => e.MiddleName).HasMaxLength(DescMaxLength).IsRequired(false);
//            b.Property(e => e.LastName).HasMaxLength(DescMaxLength).IsRequired();
//            b.Property(e => e.Theme).HasMaxLength(DescMaxLength).IsRequired(false);
//            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();
//        }
//    }
//}

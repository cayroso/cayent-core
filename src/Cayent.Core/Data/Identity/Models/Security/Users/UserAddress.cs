//using Data.Identity.Models.Users;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Data.Identity.Models.Users
//{
//    internal class UserAddress
//    {
//        public string UserAddressId { get; set; }

//        public string UserId { get; set; }
//        public virtual IdentityWebUser User { get; set; }

//        public bool Default { get; set; }

//        public string Address { get; set; }
//        public double GeoX { get; set; }
//        public double GeoY { get; set; }

//        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();
//    }

//    public static class UserAddressExtension
//    {
//        public static void ThrowIfNull(this UserAddress me)
//        {
//            if (me == null)
//                throw new ApplicationException("User Address not found.");
//        }

//        public static void ThrowIfNullOrAlreadyUpdated(this UserAddress me, string currentToken, string newToken)
//        {
//            me.ThrowIfNull();

//            if (string.IsNullOrWhiteSpace(newToken))
//                throw new ApplicationException("Anti-forgery token not found.");

//            if (me.ConcurrencyToken != currentToken)
//                throw new ApplicationException("User Address already updated by another user.");

//            me.ConcurrencyToken = newToken;
//        }
//    }

//    internal class UserAddressConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<UserAddress>
//    {
//        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<UserAddress> b)
//        {
//            b.ToTable("UserAddress");
//            b.HasKey(e => e.UserAddressId);

//            b.Property(e => e.UserAddressId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.Address).HasMaxLength(DescMaxLength).IsRequired();
//            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

//        }
//    }
//}

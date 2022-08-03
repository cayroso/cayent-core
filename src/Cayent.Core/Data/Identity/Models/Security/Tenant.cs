
//using Data.Identity.Models.Users;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.Text;

//namespace Data.Identity.Models.Security
//{
//    internal class Tenant
//    {
//        [Required]
//        public string TenantId { get; set; }

//        public string Name { get; set; }
//        public string Host { get; set; }
//        public string DatabaseConnectionString { get; set; }

//        public string PhoneNumber { get; set; }
//        public string Email { get; set; }
//        public string Address { get; set; }
//        public double GeoX { get; set; }
//        public double GeoY { get; set; }
//        public virtual ICollection<IdentityWebUser> Users { get; set; } = new List<IdentityWebUser>();
//    }

//    internal static class TenantExtension
//    {

//        public static void ThrowIfNull(this Tenant me)
//        {
//            if (me == null)
//                throw new ApplicationException("Tenant not found.");
//        }
//        //public static void ThrowIfNullOrAlreadyUpdated(this Tenant me, string currentToken, string newToken)
//        //{
//        //    me.ThrowIfNull();

//        //    if (string.IsNullOrWhiteSpace(newToken))
//        //        throw new ApplicationException("Anti-forgery token not found.");

//        //    if (me.ConcurrencyToken != currentToken)
//        //        throw new ApplicationException("Bottle already updated by another user.");

//        //    me.ConcurrencyToken = newToken;
//        //}
//    }

//    internal class TenantConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<Tenant>
//    {
//        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Tenant> b)
//        {
//            b.ToTable("Tenant");
//            b.HasKey(q => q.TenantId);

//            b.Property(e => e.TenantId).HasMaxLength(KeyMaxLength).IsRequired();
//            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
//            b.Property(e => e.Host).HasMaxLength(NameMaxLength);
//            b.Property(e => e.DatabaseConnectionString).HasMaxLength(DescMaxLength);
//            b.Property(e => e.PhoneNumber).HasMaxLength(NameMaxLength).IsRequired();
//            b.Property(e => e.Email).HasMaxLength(NameMaxLength).IsRequired();
//            b.Property(e => e.Address).HasMaxLength(DescMaxLength).IsRequired();

//            b.HasMany(e => e.Users)
//                .WithOne(d => d.Tenant)
//                .HasForeignKey(d => d.TenantId);

//        }
//    }
//}



using Cayent.Core.Data.Components;
using Cayent.Core.Data.Fileuploads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Users
{
    public abstract class UserBase
    {
        public string UserId { get; set; }
        public string ImageId { get; set; }
        public virtual FileUpload Image { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FirstLastName => $"{FirstName} {LastName}";
        [NotMapped]
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }


        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public ICollection<UserRoleBase> UserRoles { get; set; } = new List<UserRoleBase>();
    }

    public static class UserExtension
    {

        public static void ThrowIfNull(this UserBase me)
        {
            if (me == null)
                throw new ApplicationException("User not found.");
        }

        public static void ThrowIfNullOrAlreadyUpdated(this UserBase me, string currentToken, string newToken)
        {
            me.ThrowIfNull();

            if (string.IsNullOrWhiteSpace(newToken))
                throw new ApplicationException("Anti-forgery token not found.");

            if (me.ConcurrencyToken != currentToken)
                throw new ApplicationException("User already updated by another user.");

            me.ConcurrencyToken = newToken;
        }
    }

    public class UserBaseConfiguration : EntityBaseConfiguration<UserBase>
    {
        public override void Configure(EntityTypeBuilder<UserBase> b)
        {
            b.ToTable("User");
            b.HasKey(e => e.UserId);

            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.FirstName).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.LastName).HasMaxLength(NameMaxLength).IsRequired();
            b.Property(e => e.Email).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.PhoneNumber).HasMaxLength(KeyMaxLength).IsRequired();

            b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasMany(e => e.UserRoles)
                .WithOne(d => d.User)
                .HasForeignKey(fk => fk.UserId);
        }
    }

    //  EXAMPLE
    //public class User : UserBase
    //{
    //    public virtual ICollection<UserTask> UserTasks { get; set; } = new List<UserTask>();
    //}

    //public class UserConfiguration : UserConfiguration<User>
    //{
    //    public override void Configure(EntityTypeBuilder<User> builder)
    //    {
    //        base.Configure(builder);
    //        this.ConfigureEntity(builder);
    //    }

    //    private void ConfigureEntity(EntityTypeBuilder<User> builder)
    //    {            
    //    }
    //}
}

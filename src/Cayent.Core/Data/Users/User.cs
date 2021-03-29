

using Cayent.Core.Data.Fileuploads;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Data.Users
{
    [Table("User")]
    public abstract class UserBase
    {
        [Key]
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

        public virtual ICollection<UserRoleBase> UserRoles { get; set; } = new List<UserRoleBase>();
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

    public abstract class UserConfiguration<T> : IEntityTypeConfiguration<T> where T : UserBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //throw new NotImplementedException();
        }
    }
}



using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Users
{
    [Table("UserRole")]
    public abstract class UserRoleBase
    {
        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string RoleId { get; set; }
        public virtual RoleBase Role { get; set; }
    }

    public abstract class UserRoleConfiguration<T> : IEntityTypeConfiguration<T> where T : UserRoleBase
    {
        void IEntityTypeConfiguration<T>.Configure(EntityTypeBuilder<T> builder)
        {
            //throw new NotImplementedException();
        }
    }
}

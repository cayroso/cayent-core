

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Users
{
    [Table("UserRole")]
    public abstract class UserRoleBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string UserRoleId { get; set; }

        public string UserId { get; set; }
        public virtual UserBase User { get; set; }
        
        public string RoleId { get; set; }
        public virtual RoleBase Role { get; set; }
    }

    public abstract class UserRoleConfiguration<T> : IEntityTypeConfiguration<T> where T : UserRoleBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //builder.HasKey(e => new { e.UserId, e.RoleId });
            //throw new NotImplementedException();
        }
    }
}

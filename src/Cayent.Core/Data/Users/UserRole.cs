

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
        }
    }

    //  EXAMPLE
    //public class UserRole : UserRoleBase
    //{
    //    //public string UserId { get; set; }
    //    //public virtual User User { get; set; }

    //    //public string RoleId { get; set; }
    //    //public virtual Role Role { get; set; }

    //}

    //public class UserRoleConfiguration : UserRoleConfiguration<UserRole>
    //{
    //    public override void Configure(EntityTypeBuilder<UserRole> builder)
    //    {
    //        base.Configure(builder);
    //        this.ConfigureEntity(builder);
    //    }

    //    private void ConfigureEntity(EntityTypeBuilder<UserRole> builder)
    //    {            
    //    }
    //}
}

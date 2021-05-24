

using Cayent.Core.Data.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components.Users
{
    internal class UserRoleBase
    {        
        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string RoleId { get; set; }
        public virtual RoleBase Role { get; set; }
    }

    internal class UserRoleBaseConfiguration : EntityBaseConfiguration<UserRoleBase>
    {
        public override void Configure(EntityTypeBuilder<UserRoleBase> b)
        {
            b.ToTable("UserRole");
            b.HasKey(e => new { e.UserId, e.RoleId });

            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.RoleId).HasMaxLength(KeyMaxLength).IsRequired();
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

using Cayent.Core.Data.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace Cayent.Core.Data.Components.Users
{
    internal abstract class RoleBase
    {
        public string RoleId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<UserRoleBase> UserRoles { get; set; } = new List<UserRoleBase>();
    }

    internal class RoleBaseConfiguration : EntityBaseConfiguration<RoleBase>
    {
        public override void Configure(EntityTypeBuilder<RoleBase> b)
        {
            b.ToTable("Role");
            b.HasKey(e => e.RoleId);

            b.Property(e => e.RoleId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();

            b.HasMany(e => e.UserRoles)
                .WithOne(d => d.Role)
                .HasForeignKey(fk => fk.RoleId);
        }
    }

    //EXAMPLE
    //public class Role : RoleBase
    //{        
    //}

    //public class RoleConfiguration : RoleConfiguration<Role>
    //{
    //    public override void Configure(EntityTypeBuilder<Role> builder)
    //    {
    //        base.Configure(builder);
    //        this.ConfigureEntity(builder);
    //    }

    //    private void ConfigureEntity(EntityTypeBuilder<Role> builder)
    //    {            
    //    }
    //}
}

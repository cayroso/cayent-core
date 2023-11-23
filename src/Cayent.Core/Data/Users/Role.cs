using Cayent.Core.Data.Components;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Users
{
    internal abstract class RoleBase
    {
        public string RoleId { get; set; }
        public string Name { get; set; }
    }

    internal class RoleBaseConfiguration : EntityBaseConfiguration<RoleBase>
    {
        public override void Configure(EntityTypeBuilder<RoleBase> b)
        {
            b.ToTable("Role");
            b.HasKey(e => e.RoleId);

            b.Property(e => e.RoleId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();
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

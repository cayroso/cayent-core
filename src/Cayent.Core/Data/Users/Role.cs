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
    [Table("Role")]
    public abstract class RoleBase
    {
        [Key]
        public string RoleId { get; set; }
        public string Name { get; set; }
    }

    public abstract class RoleConfiguration<T> : IEntityTypeConfiguration<T> where T : RoleBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            //throw new NotImplementedException();
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

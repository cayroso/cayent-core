
using Cayent.Core.Data.Components;
using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Data.Components.BranchStores
{
    public abstract class BranchStoreUserBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string BranchStoreId { get; set; }
        public virtual BranchStoreBase BranchStore { get; set; }

        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string RoleId { get; set; }
        public virtual UserBase Role { get; set; }
    }

    public class BranchStoreUserBaseConfiguration : EntityBaseConfiguration<BranchStoreUserBase>
    {
        public override void Configure(EntityTypeBuilder<BranchStoreUserBase> b)
        {
            b.ToTable("BranchStoreUser");
            b.HasKey(e => new { e.BranchStoreId, e.UserId, e.RoleId });

            b.Property(e => e.BranchStoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.RoleId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.BranchStore.Active);
        }
    }
}

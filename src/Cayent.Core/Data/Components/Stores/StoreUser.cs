using Cayent.Core.Data.Components.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Cayent.Core.Data.Components.Stores
{
    internal class StoreUserBase
    {        
        public string StoreId { get; set; }
        public virtual StoreBase Store { get; set; }

        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string RoleId { get; set; }
        public RoleBase Role { get; set; }
    }

    internal class StoreUserBaseConfiguration : EntityBaseConfiguration<StoreUserBase>
    {
        public override void Configure(EntityTypeBuilder<StoreUserBase> b)
        {
            b.ToTable("StoreUser");
            b.HasKey(e => new { e.StoreId, e.UserId, e.RoleId });

            b.Property(e => e.StoreId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.UserId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.RoleId).HasMaxLength(KeyMaxLength).IsRequired();

            b.HasQueryFilter(e => e.Store.Active);
        }
    }
}

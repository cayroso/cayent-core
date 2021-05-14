using Cayent.Core.Data.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.Stores
{
    public abstract class StoreUserBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string StoreId { get; set; }
        public StoreBase Store { get; set; }

        public string UserId { get; set; }
        public UserBase User { get; set; }

        public string RoleId { get; set; }
        public UserBase Role { get; set; }
    }

    public class StoreUserBaseConfiguration : EntityBaseConfiguration<StoreUserBase>
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

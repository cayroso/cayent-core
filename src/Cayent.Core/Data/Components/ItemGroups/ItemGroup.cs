using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Cayent.Core.Data.Components.Items;

namespace Cayent.Core.Data.Components.ItemGroups
{
    public abstract class ItemGroupBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ItemGroupId { get; set; }
        public string Name { get; set; }

        public ICollection<ItemBase> Items { get; set; } = new List<ItemBase>();
    }

    public class ItemGroupBaseConfiguration : EntityBaseConfiguration<ItemGroupBase>
    {
        public override void Configure(EntityTypeBuilder<ItemGroupBase> b)
        {
            b.ToTable("ItemGroup");
            b.HasKey(e => e.ItemGroupId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ItemGroupId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();

            b.HasMany(e => e.Items)
                .WithOne(d => d.ItemGroup)
                .HasForeignKey(fk => fk.ItemGroupId);
        }
    }
}

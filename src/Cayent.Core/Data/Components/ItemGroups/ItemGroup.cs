using Cayent.Core.Data.Components;
using Cayent.Core.Data.Components.Products;
using Cayent.Core.Data.Components.Promotions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cayent.Core.Data.Components.ItemGroups
{
    internal abstract class ItemGroupBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string ItemGroupId { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<ProductBase> Products { get; set; } = new List<ProductBase>();
        //public virtual ICollection<PromotionProductFilterBase> PromotionFilters { get; set; } = new List<PromotionProductFilterBase>();

    }

    internal class ItemGroupBaseConfiguration : EntityBaseConfiguration<ItemGroupBase>
    {
        public override void Configure(EntityTypeBuilder<ItemGroupBase> b)
        {
            b.ToTable("ItemGroup");
            b.HasKey(e => e.ItemGroupId);
            b.HasIndex(e => e.Name).IsUnique();

            b.Property(e => e.ItemGroupId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.Name).HasMaxLength(NameMaxLength).IsRequired();

        }
    }
}

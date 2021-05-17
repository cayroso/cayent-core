using Cayent.Core.Data.Components.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cayent.Core.Data.Components.Categories
{
    public class CategoryBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CategoryId { get; set; }
        public string Name { get; set; }

        public string ParentId { get; set; }
        public virtual CategoryBase Parent { get; set; }

        public virtual ICollection<CategoryBase> Children { get; set; } = new List<CategoryBase>();
    }

    public class CategoryBaseConfiguration : EntityBaseConfiguration<CategoryBase>
    {
        public override void Configure(EntityTypeBuilder<CategoryBase> b)
        {
            b.ToTable("Category");
            b.HasKey(e => e.CategoryId);

            b.Property(e => e.CategoryId).HasMaxLength(KeyMaxLength).IsRequired();
            b.Property(e => e.ParentId).HasMaxLength(KeyMaxLength);

            b.HasMany(e => e.Children)
                .WithOne(d => d.Parent)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.Restrict);
        
        }
    }
}

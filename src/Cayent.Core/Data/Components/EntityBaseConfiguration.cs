using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cayent.Core.Data.Components
{
    public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T> where T: class
    {
        protected const int KeyMaxLength = 36;
        protected const int NameMaxLength = 256;
        protected const int DescMaxLength = 2048;
        protected const int NoteMaxLength = 4096;

        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}

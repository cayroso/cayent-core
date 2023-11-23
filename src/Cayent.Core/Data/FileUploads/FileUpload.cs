using Cayent.Core.Common.Extensions;

namespace Cayent.Core.Data.Fileuploads
{
    internal class FileUpload
    {
        public string FileUploadId { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string ContentDisposition { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }
        public long Length { get; set; }

        DateTime _dateCreated = DateTime.UtcNow;
        public DateTime DateCreated
        {
            get => _dateCreated.AsUtc();
            set => _dateCreated = value.Truncate();
        }
    }

    //public class ClinicConfiguration : Cayent.Core.Data.Components.EntityBaseConfiguration<Clinic>
    //{
    //    public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Clinic> b)
    //    {
    //        b.ToTable("Clinic");
    //        b.HasKey(e => e.ClinicId);

    //        b.Property(e => e.ClinicId).HasMaxLength(KeyMaxLength).IsRequired();
    //        b.Property(e => e.Name).HasMaxLength(NameMaxLength);

    //        b.Property(e => e.ConcurrencyToken).HasMaxLength(KeyMaxLength).IsRequired();

    //    }
    //}
}

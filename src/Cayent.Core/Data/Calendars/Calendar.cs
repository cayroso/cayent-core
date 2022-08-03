using Cayent.Core.Common.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cayent.Core.Data.Calendars
{
    internal class Calendar
    {
        public Calendar()
        {
            //JobStatusHistory = new List<JobStatusHistory>();
        }

        DateTime _date;
        public DateTime Date
        {
            get => _date.AsUtc();
            set => _date = value.Truncate();
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int Quarter { get; set; }

        public string MonthName { get; set; }
        public int DayOfYear { get; set; }
        public int DayOfWeek { get; set; }
        public string DayName { get; set; }

        //public virtual ICollection<JobStatusHistory> JobStatusHistory { get; set; }
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

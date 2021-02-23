using Cayent.Data.App.Models.Fileuploads;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Data.App.Models.Users
{
    public class User
    {
        public string UserId { get; set; }
        public string ImageId { get; set; }
        public virtual FileUpload Image { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        [NotMapped]
        public string FirstLastName => $"{FirstName} {LastName}";
        [NotMapped]
        public string Initials => $"{FirstName[0]}{LastName[0]}".ToUpper();
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public double OverallRating { get; set; }
        public double TotalRating { get; set; }

        public string ConcurrencyToken { get; set; } = Guid.NewGuid().ToString();

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        
        public void CalculateRating(double newRating)
        {
            OverallRating = ((OverallRating * TotalRating) + newRating) / (TotalRating + 1);
            TotalRating += newRating;
        }
    }
}

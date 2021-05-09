using Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Components.Customers
{
    public class CustomerAddress
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CustomerAddressId { get; set; }

        public string CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public EnumAddressType AddressType { get; set; }
        public bool IsPrimary { get; set; }
        public string Address { get; set; }
        public double GeoX { get; set; }
        public double GeoY { get; set; }
    }
}

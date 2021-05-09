using Cayent.Core.Data.Fileuploads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Components.Products
{
    public class ProductImage
    {
        public string ProductId { get; set; }
        public virtual Product Product { get; set; }

        public string ImageId { get; set; }
        public virtual FileUpload Image { get; set; }

        public uint SortOrder { get; set; }
    }
}

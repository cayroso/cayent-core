using Data.Components.Products;

namespace Data.Components.BranchStores
{
    public class BranchStoreProduct
    {
        public string BranchStoreId { get; set; }
        public virtual BranchStore BranchStore { get; set; }

        public string ProductId { get; set; }
        public virtual Product Product { get; set; }
    }
}

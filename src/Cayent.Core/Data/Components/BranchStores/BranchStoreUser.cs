
using Cayent.Core.Data.Users;

namespace Data.Components.BranchStores
{
    public class BranchStoreUser
    {
        public string BranchStoreId { get; set; }
        public virtual BranchStore BranchStore { get; set; }

        public string UserId { get; set; }
        public virtual UserBase User { get; set; }

        public string RoleId { get; set; }
        public virtual UserBase Role { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Common
{
    public abstract class PagedDto<T> where T : class
    {
        public List<T> Items { get; set; }

        public string Criteria { get; set; } = string.Empty;
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public int TotalCount { get; set; } = 0;
        public int TotalPages { get; set; } = 0;
        public int StartIndex { get; set; } = 0;
    }
}

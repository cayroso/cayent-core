using Cayent.Core.Web.Common;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Components
{
    public partial class MyPagination<TPagedDto, TItem>
        where TPagedDto : PagedDto<TItem>
        where TItem : class
    {
        [Parameter] public TPagedDto PagedItems { get; set; }
        [Parameter] public string NoRecords { get; set; } = "No records found.";
        [Parameter] public bool ShowPerPage { get; set; } = true;
        [Parameter] public EventCallback<int> SearchCallBack { get; set; }

        protected async Task ChangePageSize(ChangeEventArgs e)
        {
            PagedItems.PageSize = int.Parse(e.Value.ToString());
            await SearchAsync(1);
        }

        protected async Task SearchAsync(int pageIndex)
        {
            await SearchCallBack.InvokeAsync(pageIndex);
        }

        protected bool CanMoveFirst => PagedItems.PageIndex > 1;
        protected bool CanMovePrev => PagedItems.PageIndex > 1;
        protected bool CanMoveNext => PagedItems.PageIndex < PagedItems.TotalPages;
        protected bool CanMoveLast => PagedItems.PageIndex < PagedItems.TotalPages;

        protected async Task MoveFirst()
        {
            if (CanMoveFirst)
            {
                PagedItems.PageIndex = 1;
                await SearchAsync(PagedItems.PageIndex);
            }
        }

        protected async Task MovePrev()
        {
            if (CanMovePrev)
            {
                PagedItems.PageIndex--;
                await SearchAsync(PagedItems.PageIndex);
            }
        }

        protected async Task MoveNext()
        {
            if (CanMoveNext)
            {
                PagedItems.PageIndex++;
                await SearchAsync(PagedItems.PageIndex);
            }
        }

        protected async Task MoveLast()
        {
            if (CanMoveLast)
            {
                PagedItems.PageIndex = PagedItems.TotalPages;
                await SearchAsync(PagedItems.PageIndex);
            }
        }
    }
}

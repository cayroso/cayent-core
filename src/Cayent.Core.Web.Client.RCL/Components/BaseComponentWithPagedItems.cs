using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cayent.Core.Web.Client.RCL.Components
{
    /// <summary>
    /// Base Component with Paged Items, Inheriting base component With Http Interceptor + With Cancellation Token
    /// </summary>
    public abstract class BaseComponentWithPagedItems<TPagedDto, T> : BaseComponentWithHttpInterceptor
        where TPagedDto : Common.PagedDto<T>
        where T : class
    {
        protected TPagedDto PagedItems { get; set; }

        protected T SelectedItem { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        public override void Dispose()
        {
            base.Dispose();
        }

        protected abstract Task<TPagedDto> SearchAsync(int pageIndex, int pageSize, string criteria, string sortField, int sortOrder, CancellationToken cancellationToken);

        protected string Criteria { get; set; }
        protected string SortField { get; set; }
        protected int SortOrder { get; set; }

        protected bool CanMoveFirst => PagedItems.PageIndex > 1;
        protected bool CanMovePrev => PagedItems.PageIndex - 1 > 1;
        protected bool CanMoveNext => PagedItems.PageIndex < PagedItems.TotalCount - 1;
        protected bool CanMoveLast => PagedItems.PageIndex < PagedItems.TotalCount;

        protected async Task<TPagedDto> SearchAsync(int pageIndex, int pageSize)
        {
            PagedItems = await SearchAsync(pageIndex, pageSize, Criteria, SortField, SortOrder, CancellationToken);

            return PagedItems;
        }

        protected async Task MoveFirst()
        {
            PagedItems.PageIndex = 1;
            await SearchAsync(PagedItems.PageIndex, PagedItems.PageSize);

        }

        protected async Task MovePrev()
        {
            PagedItems.PageIndex--;
            await SearchAsync(PagedItems.PageIndex, PagedItems.PageSize);
        }

        protected async Task MoveNext()
        {
            PagedItems.PageIndex++;
            await SearchAsync(PagedItems.PageIndex, PagedItems.PageSize);
        }

        protected async Task MoveLast()
        {
            PagedItems.PageIndex = PagedItems.TotalPages;
            await SearchAsync(PagedItems.PageIndex, PagedItems.PageSize);
        }

        protected int GetItemIndex(T dto)
        {
            return PagedItems.Items.IndexOf(dto);
        }

        protected void SetSelectedItem(T item)
        {
            if (SelectedItem == item)
                SelectedItem = null;
            else
                SelectedItem = item;
        }

        protected bool IsSelectedItem(T item)
        {
            return SelectedItem == item;
        }
    }
}

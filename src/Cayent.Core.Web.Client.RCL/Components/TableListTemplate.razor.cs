using Microsoft.AspNetCore.Components;
using System.Diagnostics.CodeAnalysis;

namespace Cayent.Core.Web.Client.RCL.Components
{
    public partial class TableListTemplate<TItem> where TItem : class
    {
        [Parameter, AllowNull] public IList<TItem> Items { get; set; }

        [Parameter] public string Breakpoint { get; set; } = "sm";

        [Parameter] public string TableCss { get; set; } = "table";
        [Parameter] public string TableHeaderCss { get; set; }
        [Parameter] public string TableFooterCss { get; set; }

        [Parameter] public string ListCss { get; set; } = "";
        [Parameter] public string ListContainerCss { get; set; } = "row";
        [Parameter] public string ListItemCss { get; set; } = "col-12";
        [Parameter] public string ListItemNotSelectedCss { get; set; } = "border p-2";
        [Parameter] public string ListItemSelectedCss { get; set; } = "border p-2 shadow shadow-sm my-1";
        

        [Parameter] public RenderFragment? TableHeader { get; set; }
        [Parameter] public RenderFragment<TItem>? RowTemplate { get; set; }
        [Parameter] public RenderFragment<TItem>? ListTemplate { get; set; }
        [Parameter] public RenderFragment? TableFooter { get; set; }

        [Parameter] public EventCallback<TItem> FuncSetSelectedItem { get; set; }

        [Parameter] public Func<TItem, bool> FuncIsSelectedItem { get; set; }

        async Task SetSelectedItem(TItem item)
        {
            await FuncSetSelectedItem.InvokeAsync(item);
        }

        bool IsItemSelected(TItem item)
        {
            if (FuncIsSelectedItem == null)
                return false;

            var result = FuncIsSelectedItem.Invoke(item);
            
            return result;
        }
    }
}

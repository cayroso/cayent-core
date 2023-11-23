namespace Cayent.Core.Common
{
    public class Paged<T> where T : class
    {
        public Paged(IEnumerable<T> items, int totalCount, int pageIndex, int pageSize)
        {
            if (pageIndex < 1)
                throw new ArgumentOutOfRangeException(nameof(pageIndex));
            if (pageSize < 1)
                throw new ArgumentOutOfRangeException(nameof(pageSize));

            Items = items;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageIndex = pageIndex;
        }

        public IEnumerable<T> Items { get; }
        public int PageIndex { get; }
        public int PageSize { get; }
        public int TotalCount { get; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        //public bool HasPreviousPage => PageIndex > 1;
        //public bool HasNextPage => PageIndex < TotalPages;
        public int StartIndex => (PageIndex - 1) * PageSize;

        //public IEnumerator<T> GetEnumerator() => _items.GetEnumerator();
        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}

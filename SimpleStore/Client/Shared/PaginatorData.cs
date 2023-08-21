namespace SimpleStore.Client.Shared
{
    public class PaginatorData
    {
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; }
        public int Total { get; set; }
        public int Count { get; set; }
    }

    public class PagedData<T> : PaginatorData
    {
        public ICollection<T> Data { get; set; }
        public PagedData() => Data = new List<T>();
    }
}

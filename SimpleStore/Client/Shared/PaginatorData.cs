namespace SimpleStore.Client.Shared
{
    public class PaginatorData
    {
        public int CurrentPage { get; set; }
        public int RowsPerPage { get; set; }
        public int TotalPages { get; set; }
        public int Count { get; set; }
    }

    public class Pages<T> : PaginatorData
    {
        public ICollection<T> Data { get; set; }
        
        public Pages()
        {
            Data = new List<T>();
        }
    }
}

namespace SimpleStore.Shared.Response
{
    public class ResponsePagination<T> : ResponseBase
    {
        public ICollection<T>? Data { get; set; }
        public int Pages { get; set; }
    }
}

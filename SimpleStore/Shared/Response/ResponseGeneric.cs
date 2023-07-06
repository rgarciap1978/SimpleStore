namespace SimpleStore.Shared.Response
{
    public class ResponseGeneric<T> : ResponseBase
    {
        public T? Data { get; set; }
        public int Pages { get; set; }
    }
}

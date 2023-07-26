namespace SimpleStore.Shared.Response
{
    public class ResponseDTOLogin : ResponseBase
    {
        public string Token { get; set; } = default!;
        public string FullName { get; set; } = default!;
        public List<string> Roles { get; set; } = default!;
    }
}

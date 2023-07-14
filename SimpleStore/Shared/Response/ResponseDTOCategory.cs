namespace SimpleStore.Shared.Response
{
    public class ResponseDTOCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public bool Status { get; set; }
        public string StringStatus => Status ? "Activo" : "Inactivo";
    }
}

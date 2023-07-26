using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Shared.Request
{
    public class RequestDTOLogin
    {
        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;
    }
}

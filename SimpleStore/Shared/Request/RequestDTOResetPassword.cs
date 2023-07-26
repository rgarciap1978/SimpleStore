using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Shared.Request
{
    public class RequestDTOResetPassword
    {
        public string Token { get; set; } = default!;
        public string Email { get; set; } = default!;

        [Required]
        public string NewPassword { get; set; } = default!;

        [Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; } = default!;
    }
}

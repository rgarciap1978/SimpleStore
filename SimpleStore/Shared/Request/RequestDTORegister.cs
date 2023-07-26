using System.ComponentModel.DataAnnotations;

namespace SimpleStore.Shared.Request
{
    public class RequestDTORegister
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string LastName { get; set; } = default!;

        [Required]
        public string Password { get; set; } = default!;

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; } = default!;
    }
}

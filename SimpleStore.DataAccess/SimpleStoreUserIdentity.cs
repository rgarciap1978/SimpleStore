using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SimpleStore.DataAccess
{
    public class SimpleStoreUserIdentity : IdentityUser
    {
        [StringLength(100)]
        public string FirstName { get; set; } = default!;
        
        [StringLength(100)]
        public string LastName { get; set; } = default!;
    }
}

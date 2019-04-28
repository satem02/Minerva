using Microsoft.AspNetCore.Identity;

namespace Minerva.Shared.Data.Entities
{
    public class UserEntity : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
    }
}
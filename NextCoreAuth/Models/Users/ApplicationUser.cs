using System;
using Microsoft.AspNetCore.Identity;

namespace Models.Users
{
    public class ApplicationUser : IdentityUser<long>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string locale { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset LastLoggedIn { get; set; }
        public bool IsActive { get; set; }
        public string PhotoUri { get; set; }
    }
}
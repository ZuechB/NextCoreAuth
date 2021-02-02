using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Users;

namespace Services.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, Role, long>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder builder)
        {




            // keep at the bottom
            base.OnModelCreating(builder);
        }
    }
}

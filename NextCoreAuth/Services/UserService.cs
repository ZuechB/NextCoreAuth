using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using Services.Context;
using Microsoft.EntityFrameworkCore;
using Models.Users;

namespace Services
{
    public interface IUserService
    {
        Task<ApplicationUser> GetSignedInUser();
    }

    public class UserService : IUserService
    {
        readonly IHttpContextAccessor httpContextAccessor;
        readonly DatabaseContext databaseContext;

        public UserService(IHttpContextAccessor httpContextAccessor, DatabaseContext databaseContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.databaseContext = databaseContext;
        }

        public async Task<ApplicationUser> GetSignedInUser()
        {
            var nameClaim = httpContextAccessor.HttpContext.User.Claims.Where(c => c.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            if (nameClaim != null)
            {
                var userId = Convert.ToInt64(nameClaim.Value);

                var usr = await databaseContext.Users.Where(u => u.Id == userId).FirstOrDefaultAsync();
                if (usr != null)
                {
                    return usr;
                }
            }

            return null;
        }
    }
}

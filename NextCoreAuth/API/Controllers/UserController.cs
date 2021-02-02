using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var signedInUser = await userService.GetSignedInUser();
            if (signedInUser == null)
            {
                return Ok(null);
            }
            else
            {
                return Ok(new
                {
                    firstName = signedInUser.FirstName,
                    lastName = signedInUser.LastName,
                    email = signedInUser.Email
                });
            }
        }

    }
}

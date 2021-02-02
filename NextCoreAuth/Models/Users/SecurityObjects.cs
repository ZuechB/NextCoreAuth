﻿using Microsoft.AspNetCore.Identity;

namespace Models.Users
{
    public partial class UserLogin : IdentityUserLogin<long>
    {
    }


    public partial class UserClaim : IdentityUserClaim<long>
    {

    }

    public partial class Role : IdentityRole<long>
    {

    }

    public partial class UserRole : IdentityUserRole<long> { }
}
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MyCosts
{
    public class User : IdentityUser
    {
        public User()
        {
            Costs = new HashSet<Cost>();
        //    UserRoles = new HashSet<UserRole>();
        }

        ////public int Id { get; set; }
        ////public string Email { get; set; }
        ////public string Password { get; set; }

        public virtual ICollection<Cost> Costs { get; set; }
        //public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

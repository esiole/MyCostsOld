﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyCosts.ViewModels
{
    public class ChangeRole
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        
        public ChangeRole()
        {
            AllRoles = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}

using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyCosts.Models
{
    public class User : IdentityUser
    {
        public virtual ICollection<Cost> Costs { get; set; }

        public User()
        {
            Costs = new HashSet<Cost>();
        }
    }
}

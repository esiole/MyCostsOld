using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCosts
{
    public class AddCost
    {
        public SelectList Products { get; set; }
        public SelectList Categories { get; set; }
        public Cost Cost { get; set; }
    }
}

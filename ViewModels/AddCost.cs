using Microsoft.AspNetCore.Mvc.Rendering;
using MyCosts.Models;

namespace MyCosts
{
    public class AddCost
    {
        public SelectList Products { get; set; }
        public SelectList Categories { get; set; }
        public Cost Cost { get; set; }
    }
}

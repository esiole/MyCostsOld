using Microsoft.AspNetCore.Mvc.Rendering;
using MyCosts.Models;

namespace MyCosts
{
    public class AddProduct
    {
        public Product Product { get; set; }
        public SelectList Categories { get; set; }
    }
}

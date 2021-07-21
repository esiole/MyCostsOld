using Microsoft.AspNetCore.Mvc;

namespace MyCosts.Components
{
    public class NonAuthorized : ViewComponent
    {
        public IViewComponentResult Invoke() => View();
    }
}

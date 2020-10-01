using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;

namespace MyCosts
{
    [ViewComponent]
    public class NonAuthorized
    {
        public IViewComponentResult Invoke()
        {
            return new HtmlContentViewComponentResult(
                new HtmlString($"<div><p><b>Авторизуйтесь, чтобы использовать приложение!</b></p></div>")
            );
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace MyCosts.TagHelpers
{
    [HtmlTargetElement("pagination")]
    public class PaginationTagHelper : TagHelper
    {
        private readonly IUrlHelperFactory urlHelperFactory;

        [HtmlAttributeName("contoller-action")]
        public string Action { get; set; }

        [HtmlAttributeName("count-records")]
        public int Count { get; set; }

        [HtmlAttributeName("page")]
        public int Page { get; set; }

        [HtmlAttributeName("per-page")]
        public int PerPage { get; set; }

        [HtmlAttributeName("save-query")]
        public bool IsSaveQuery { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PaginationTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            int countPage = (int)Math.Ceiling(Count / (double)PerPage);

            TagBuilder tag = new("ul");
            tag.AddCssClass("pagination justify-content-center");
            tag.InnerHtml.AppendHtml(CreatePaginationListItem("&laquo;", Page == 1 ? 1 : Page - 1, urlHelper, Page == 1));
            if (countPage < 8)
            {   // Если страниц меньше 8, то "..." не появляются при их переключении
                int currentPage = 1;
                while (currentPage <= countPage)
                {
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(currentPage, urlHelper));
                    currentPage++;
                }
            } 
            else
            {   // Отображаются страницы с номерами 1, 2, предпоследним, последним, а также три номера, вокруг активного - в центре
                tag.InnerHtml.AppendHtml(CreatePaginationListItem(1, urlHelper));
                tag.InnerHtml.AppendHtml(CreatePaginationListItem(2, urlHelper));

                if (Page <= 4)
                {
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(3, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(4, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(5, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem("...", 1, urlHelper, disabled:true));
                }
                else if (Page >= countPage - 3)
                {
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem("...", 1, urlHelper, disabled:true));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(countPage - 4, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(countPage - 3, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(countPage - 2, urlHelper));
                }
                else
                {
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem("...", 1, urlHelper, disabled: true));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(Page - 1, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(Page, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem(Page + 1, urlHelper));
                    tag.InnerHtml.AppendHtml(CreatePaginationListItem("...", 1, urlHelper, disabled: true));
                }

                tag.InnerHtml.AppendHtml(CreatePaginationListItem(countPage - 1, urlHelper));
                tag.InnerHtml.AppendHtml(CreatePaginationListItem(countPage, urlHelper));
            }
            tag.InnerHtml.AppendHtml(CreatePaginationListItem("&raquo;", Page == countPage ? countPage : Page + 1, urlHelper, Page == countPage));

            output.Content.AppendHtml(tag);
        }

        private TagBuilder CreatePaginationListItem(int page, IUrlHelper urlHelper)
        {
            TagBuilder item = new("li");
            TagBuilder link = new("a");
            if (page == Page)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = CreateLinkHref(page, urlHelper);
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");;
            link.InnerHtml.Append(page.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }

        private TagBuilder CreatePaginationListItem(string str, int page, IUrlHelper urlHelper, bool disabled=false)
        {
            TagBuilder item = new("li");
            TagBuilder link = new("a");
            link.Attributes["href"] = CreateLinkHref(page, urlHelper);
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.AppendHtml(str);
            item.InnerHtml.AppendHtml(link);
            if (disabled)
            {
                item.AddCssClass("disabled");
            }
            return item;
        }

        private string CreateLinkHref(int page, IUrlHelper urlHelper)
        {
            string href = urlHelper.Action(Action ?? "Index", new { page });

            // Сохранение имеющихся параметров запроса 
            if (IsSaveQuery)
            {
                var query = ViewContext.HttpContext.Request.Query;
                foreach (var key in query.Keys)
                {
                    if (key == "page")
                    {
                        continue;
                    }
                    href += $"&{key}={query[key]}";
                }
            }

            return href;
        }
    }
}

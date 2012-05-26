namespace TiendaVirtualMVC.Web.HtmlHelpers
{
    using System.Text;
    using System.Web.Mvc;

    using TiendaVirtualMVC.Web.Pagination;

    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper helper,
                                               PagedList pagedList,
                                               object categoria)
        {
            StringBuilder liHtml = new StringBuilder();

            if (pagedList.HasPreviusPage)
            {
                TagBuilder liTag = CreateLi(helper, pagedList.CurrentPage - 1,
                                           "&lt;&lt;", categoria);
                liHtml.AppendLine(liTag.ToString());
            }

            for (int i = 1; i <= pagedList.TotalPages; i++)
            {
                TagBuilder liTag = CreateLi(helper, i, i.ToString(), categoria);
                if (pagedList.CurrentPage == i)
                {
                    liTag = new TagBuilder("li");
                    liTag.InnerHtml = i.ToString();
                    liTag.AddCssClass("active");
                }
                liHtml.AppendLine(liTag.ToString());
            }

            if (pagedList.HasNextPage)
            {
                TagBuilder liTag = CreateLi(helper, pagedList.
                           CurrentPage + 1, "&gt;&gt;", categoria);
                liHtml.AppendLine(liTag.ToString());
            }
            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.InnerHtml = liHtml.ToString();

            return MvcHtmlString.Create(ulTag.ToString());
        }

        private static TagBuilder CreateLi(HtmlHelper html,
                                            int pageNumber,
                                            string text,
                                            object categoria)
        {

            UrlHelper urlHelper = new UrlHelper(html.ViewContext.RequestContext);
            var url = urlHelper.Action("index", new{ pagina = pageNumber, categoria});
            if (categoria == null)
            {
                url = urlHelper.Action("index", new { pagina = pageNumber });
            }

            TagBuilder aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", url);
            aTag.InnerHtml = text;

            TagBuilder liTag = new TagBuilder("li");
            liTag.InnerHtml = aTag.ToString();
            return liTag;
        }
    }
}
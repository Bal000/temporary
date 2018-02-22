using System.Collections.Generic;
using System.Web.Mvc;
using MyWebApp.Models;
using System.Linq;
using System.Text;

namespace MyWebApp.Extensions.HtmlExtensions
{
    public static class HtmlPagination
    {
        private static readonly int _numberOfPagesToShow = 10;
        private static int _numberOfPages;

        public static MvcHtmlString RenderPagination(this HtmlHelper helper, string url, PageViewModel pageViewModel, int rowCount)
        {
            _numberOfPages = rowCount / pageViewModel.PageSize;
            List<int> pages = new List<int>();

            for (var i = 0; i < rowCount; i++)
            {
                pages.Add(i);
            }
            pages = pages.Skip(pageViewModel.Page).Take(_numberOfPagesToShow).ToList();

            StringBuilder sb = new StringBuilder();

            AddPreviousButtonIfPageIsBiggerThenOne(pageViewModel, sb);

            foreach (var page in pages)
            {
                if (pageViewModel.Page == page)
                {
                    sb.Append(string.Format("<div class=\"numeric active\">{0}</div>", page));
                }
                else
                {
                    sb.Append(string.Format("<div class=\"numeric\">{0}</div>", page));
                }
            }

            AddNextButtonIfPageIsLessThenRowCount(pageViewModel, rowCount, sb);

            return new MvcHtmlString(sb.ToString());
        }

        private static void AddNextButtonIfPageIsLessThenRowCount(PageViewModel pageViewModel, int rowCount, StringBuilder sb)
        {
            if (pageViewModel.Page < rowCount)
            {
                sb.Append("<div id=\"paginaton-next\">Next</div>");
            }
        }

        private static void AddPreviousButtonIfPageIsBiggerThenOne(PageViewModel pageViewModel, StringBuilder sb)
        {
            if (pageViewModel.Page > 1)
            {
                sb.Append("<div id=\"paginaton-previous\">Previous</div>");
            }
        }
    }
}
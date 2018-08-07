using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace OpenOrderFramework.Class
{
    public  static class Helpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html, Paging Paging, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            string anchorInnerHtml = "";
            for (int i = 1; i <= Paging.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                anchorInnerHtml = AnchorInnerHtml(i, Paging);

                if (anchorInnerHtml == "..")
                    tag.MergeAttribute("href", "#");
                else
                    tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = anchorInnerHtml;
                if (i == Paging.CurrentPage)
                {
                    tag.AddCssClass("active");
                }
                tag.AddCssClass("paging");
                if (anchorInnerHtml != "")
                    result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
        public static string AnchorInnerHtml(int i, Paging Paging)
        {
            string anchorInnerHtml = "";
            if (Paging.TotalPages <= 10)
                anchorInnerHtml = i.ToString();
            else
            {
                if (Paging.CurrentPage <= 5)
                {
                    if ((i <= 8) || (i == Paging.TotalPages))
                        anchorInnerHtml = i.ToString();
                    else if (i == Paging.TotalPages - 1)
                        anchorInnerHtml = "..";
                }
                else if ((Paging.CurrentPage > 5) && (Paging.TotalPages - Paging.CurrentPage >= 5))
                {
                    if ((i == 1) || (i == Paging.TotalPages) || ((Paging.CurrentPage - i >= -3) && (Paging.CurrentPage - i <= 3)))
                        anchorInnerHtml = i.ToString();
                    else if ((i == Paging.CurrentPage - 4) || (i == Paging.CurrentPage + 4))
                        anchorInnerHtml = "..";
                }
                else if (Paging.TotalPages - Paging.CurrentPage < 5)
                {
                    if ((i == 1) || (Paging.TotalPages - i <= 7))
                        anchorInnerHtml = i.ToString();
                    else if (Paging.TotalPages - i == 8)
                        anchorInnerHtml = "..";
                }
            }
            return anchorInnerHtml;
        }
    }
}
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace SimpleCMS.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString SubmitButton(this HtmlHelper helpers, string text)
        {
            var result = string.Format(@"<p><input type=""submit"" class=""button white"" value=""{0}""></p>", text);
            return new HtmlString(result);
        }

        public static IHtmlString FriendlyDate(this HtmlHelper helpers, DateTime date)
        {
            var result = date.ToShortTimeString();
            return new HtmlString(result);
        }

        //public static IHtmlString DeleteLink(this HtmlHelper html, string linkText, int id)
        //{
        //    //<a href="/controller/delete/1" onclick="$.post(this.href); return false;">Delete</a>
        //    var imageTag = new TagBuilder("a");
        //    imageTag.MergeAttribute("type", "submit");
        //    imageTag.MergeAttribute("class", "button white");
        //    imageTag.MergeAttribute("value", text);

        //    return new HtmlString(imageTag);
        //}

        public static IHtmlString DeleteLink(this HtmlHelper html, string linkText, int id, string title)
        {
            var confirmText = string.Format("Are you sure you want to delete {0}?", title);
            var javascript = string.Format("deleteRecord(this.href,'{0}');return false;", confirmText);

            return html.RouteLink(linkText, new { id, action = "delete" }, new { onclick = javascript });
        }
    }
}
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using SimpleCMS.Core.Models;
using TextHelper;

namespace SimpleCMS.Helpers
{
    public static class HtmlHelpers
    {
        public static IHtmlString Submit(this HtmlHelper html, string text)
        {
            var result = string.Format(@"<p><input type=""submit"" class=""button white"" value=""{0}""></p>", text);
            return new HtmlString(result);
        }

        public static IHtmlString DeleteLink(this HtmlHelper html, string linkText, int id, string title)
        {
            var confirmText = string.Format("Are you sure you want to delete {0}?", title);
            var javascript = string.Format("deleteRecord(this.href,'{0}');return false;", confirmText);

            return html.RouteLink(linkText, new { id, action = "delete" }, new { onclick = javascript });
        }

        public static IHtmlString FriendlyDate(this HtmlHelper html, DateTime date)
        {
            var result = date.ToShortTimeString();
            return new HtmlString(result);
        }

        public static IHtmlString PostSearchResult(this HtmlHelper html, Post post, string query)
        {
            var result = string.Empty;
            var titleMatch = post.Title.Excerpt(query, 30).Highlight(query);
            var bodyMatch = post.Body.Excerpt(query, 30).Highlight(query);
            return new HtmlString(titleMatch + bodyMatch);
        }
    }
}
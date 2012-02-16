using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace NGravatar.Html
{
    public static class HtmlHelperExtenions
    {
        private static Dictionary<string, string> DictionaryFromObject(object htmlAttributes)
        {
            var routes = new System.Web.Routing.RouteValueDictionary(htmlAttributes);
            var dict = new Dictionary<string, string>();
            foreach (var kvp in routes)
                dict.Add(kvp.Key, kvp.Value.ToString());   
            return dict;
        }
        
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper, 
            string email,
            int? size,
            string defaultImage,
            Rating? maxRating,
            Dictionary<string, string> htmlAttributes
        ) {
            var gravatar = new Gravatar();
            gravatar.DefaultImage = defaultImage;
            if (size.HasValue) gravatar.Size = size.Value;
            if (maxRating.HasValue) gravatar.MaxRating = maxRating.Value;
            
            return MvcHtmlString.Create(gravatar.Render(email, htmlAttributes));
        }
        
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string email,
            int? size,
            string defaultImage,
            Rating? maxRating,
            object htmlAttributes
        ) {
            return Gravatar(htmlHelper, email, size, defaultImage, maxRating, DictionaryFromObject(htmlAttributes));
        }
        
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string email,
            int? size
        ) {
            return Gravatar(htmlHelper, email, size, null, null, null);
        }
        
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string email,
            int? size,
            Dictionary<string, string> htmlAttributes
        ) {
            return Gravatar(htmlHelper, email, size, null, null, htmlAttributes);
        }
        
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string email,
            int? size,
            object htmlAttributes
        ) {            
            return Gravatar(htmlHelper, email, size, DictionaryFromObject(htmlAttributes));
        }
    }
}

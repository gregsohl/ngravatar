using System;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;

namespace NGravatar.Html {

    /// <summary>
    /// MVC HtmlHelper extension methods for rendering Gravatar avatar images.
    /// </summary>
    public static class GravatarHtml {

        private static IDictionary<string, object> CreateDictionary(object htmlAttributes) {
            if (null == htmlAttributes) return null;

            var collection = htmlAttributes as IEnumerable<KeyValuePair<string, object>>;
            return (collection ?? new RouteValueDictionary(htmlAttributes))
                .ToDictionary(pair => 
                    pair.Key, 
                    pair => pair.Value
                );
        }

        internal static GravatarProfile GravatarProfile {
            get {
                if (null == _GravatarProfile) _GravatarProfile = new GravatarProfile();
                return _GravatarProfile;
            }
            set {
                if (null == value) throw new ArgumentNullException("GravatarProfile");
                _GravatarProfile = value;
            }
        }
        private static GravatarProfile _GravatarProfile;

        /// <summary>
        /// Gets an <c>img</c> tag of the Gravatar for the supplied email address and parameters.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the rendered Gravatar.</param>
        /// <param name="rating">The maximum Gravatar rating to allow for rendered Gravatars.</param>
        /// <param name="default">The default image to display if no Gravatar exists for the specified <paramref name="emailAddress"/>.</param>
        /// <param name="forceDefault"><c>true</c> to always display the default image. Otherwise, <c>false</c>.</param> 
        /// <param name="useHttps"><c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered <c>img</c> tag.</param>
        /// <returns>An HTML string of the rendered <c>img</c> tag.</returns>
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string emailAddress,
            int? size = null,
            GravatarRating? rating = null,
            string @default = null,
            bool? forceDefault = null,
            bool? useHttps = null,
            object htmlAttributes = null
        ) {
            return MvcHtmlString.Create(
                NGravatar.Gravatar.DefaultInstance.Render(
                    emailAddress,
                    size,
                    rating,
                    @default,
                    forceDefault,
                    useHttps,
                    CreateDictionary(htmlAttributes)
                )
            );
        }

        /// <summary>
        /// Gets a link tag of the Gravatar profile for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar profile link should be rendered.</param>
        /// <param name="linkText">The link text to display.</param>
        /// <param name="useHttps"><c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered tag.</param>
        /// <returns>An HTML string of the rendered link tag.</returns>
        public static MvcHtmlString GravatarProfileLink(this HtmlHelper htmlHelper, string emailAddress, string linkText, bool? useHttps = null, object htmlAttributes = null) {
            return MvcHtmlString.Create(GravatarProfile.RenderLink(emailAddress, linkText, useHttps, CreateDictionary(htmlAttributes)));
        }

        /// <summary>
        /// Renders a script tag with the specified <paramref name="callback"/> for handling the profile JSON data.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that renders the script tag.</param>
        /// <param name="emailAddress">The email of the Gravatar profile whose JSON data should be used by the <paramref name="callback"/>.</param>
        /// <param name="callback">A JavaScript function that will be called with the JSON data as a parameter when it is rendered.</param>
        /// <param name="useHttps"><c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.</param>
        /// <returns>An HTML script tag that can be included in a page.</returns>
        public static MvcHtmlString GravatarProfileScript(this HtmlHelper htmlHelper, string emailAddress, string callback, bool? useHttps = null) {
            return MvcHtmlString.Create(GravatarProfile.RenderScript(emailAddress, callback, useHttps));
        }
    }
}

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
            return htmlAttributes == null
                ? null
                : new RouteValueDictionary(htmlAttributes)
                    .ToDictionary(
                        pair => pair.Key, 
                        pair => pair.Value
                    );
        }

        internal static GravatarProfile GravatarProfileInstance {
            get {
                if (null == _GravatarProfileInstance) _GravatarProfileInstance = new GravatarProfile();
                return _GravatarProfileInstance;
            }
            set {
                if (null == value) throw new ArgumentNullException("GravatarProfileInstance");
                _GravatarProfileInstance = value;
            }
        }
        private static GravatarProfile _GravatarProfileInstance;

        /// <summary>
        /// Gets or sets the instance of <see cref="T:Gravatar"/> to use
        /// when rendering HTML.
        /// </summary>
        public static Gravatar GravatarInstance {
            get {
                if (null == _GravatarInstance) _GravatarInstance = NGravatar.Gravatar.DefaultInstance;
                return _GravatarInstance;
            }
            set {
                if (null == value) throw new ArgumentNullException("GravatarInstance");
                _GravatarInstance = value;
            }
        }
        private static Gravatar _GravatarInstance;

        /// <summary>
        /// Gets an <c>img</c> tag of the Gravatar for the supplied email address and parameters.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the rendered Gravatar.</param>
        /// <param name="default">The default image to display if no Gravatar exists for the specified <paramref name="emailAddress"/>.</param>
        /// <param name="rating">The maximum Gravatar rating to allow for rendered Gravatars.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered <c>img</c> tag.</param>
        /// <returns>An HTML string of the rendered <c>img</c> tag.</returns>
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string emailAddress,
            int? size,
            string @default,
            GravatarRating? rating,
            IDictionary<string, object> htmlAttributes
        ) {
            return MvcHtmlString.Create(
                GravatarInstance.Render(
                    emailAddress,
                    size: size,
                    @default: @default,
                    rating: rating,
                    htmlAttributes: htmlAttributes
                )
            );
        }

        /// <summary>
        /// Gets an <c>img</c> tag of the Gravatar for the supplied email address and parameters.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the rendered Gravatar.</param>
        /// <param name="default">The default image to display if no Gravatar exists for the specified <paramref name="emailAddress"/>.</param>
        /// <param name="rating">The maximum Gravatar rating to allow for rendered Gravatars.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered <c>img</c> tag.</param>
        /// <returns>An HTML string of the rendered <c>img</c> tag.</returns>
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string emailAddress,
            int? size,
            string @default,
            GravatarRating? rating,
            object htmlAttributes
        ) {
            return Gravatar(htmlHelper, emailAddress, size, @default, rating, CreateDictionary(htmlAttributes));
        }

        /// <summary>
        /// Gets an <c>img</c> tag of the Gravatar for the supplied email address and parameters.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the rendered Gravatar.</param>
        /// <returns>An HTML string of the rendered <c>img</c> tag.</returns>
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string emailAddress,
            int? size
        ) {
            return Gravatar(htmlHelper, emailAddress, size, null, null, null);
        }

        /// <summary>
        /// Gets an <c>img</c> tag of the Gravatar for the supplied email address and parameters.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the rendered Gravatar.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered <c>img</c> tag.</param>
        /// <returns>An HTML string of the rendered <c>img</c> tag.</returns>
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string emailAddress,
            int? size,
            IDictionary<string, string> htmlAttributes
        ) {
            return Gravatar(htmlHelper, emailAddress, size, null, null, htmlAttributes);
        }

        /// <summary>
        /// Gets an <c>img</c> tag of the Gravatar for the supplied email address and parameters.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the rendered Gravatar.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered <c>img</c> tag.</param>
        /// <returns>An HTML string of the rendered <c>img</c> tag.</returns>
        public static MvcHtmlString Gravatar(
            this HtmlHelper htmlHelper,
            string emailAddress,
            int? size,
            object htmlAttributes
        ) {
            return Gravatar(htmlHelper, emailAddress, size, CreateDictionary(htmlAttributes));
        }

        /// <summary>
        /// Gets a link tag of the Gravatar profile for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="emailAddress">The email address whose Gravatar profile link should be rendered.</param>
        /// <param name="linkText">The link text to display.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered tag.</param>
        /// <returns>An HTML string of the rendered link tag.</returns>
        public static MvcHtmlString GravatarProfileLink(this HtmlHelper htmlHelper, string emailAddress, string linkText, IDictionary<string, object> htmlAttributes) {
            return MvcHtmlString.Create(GravatarProfileInstance.RenderLink(emailAddress, linkText, htmlAttributes));
        }

        /// <summary>
        /// Gets a link tag of the Gravatar profile for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="linkText">The link text to display.</param>
        /// <param name="emailAddress">The email address whose Gravatar profile link should be rendered.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the rendered tag.</param>
        /// <returns>An HTML string of the rendered link tag.</returns>
        public static MvcHtmlString GravatarProfileLink(this HtmlHelper htmlHelper, string emailAddress, string linkText, object htmlAttributes) {
            return GravatarProfileLink(htmlHelper, emailAddress, linkText, CreateDictionary(htmlAttributes));
        }

        /// <summary>
        /// Gets a link tag of the Gravatar profile for the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="linkText">The link text to display.</param>
        /// <param name="htmlHelper">The HtmlHelper object that does the rendering.</param>
        /// <param name="email">The email address whose Gravatar profile link should be rendered.</param>
        /// <returns>An HTML string of the rendered link tag.</returns>
        public static MvcHtmlString GravatarProfileLink(this HtmlHelper htmlHelper, string emailAddress, string linkText) {
            return GravatarProfileLink(htmlHelper, emailAddress, linkText, default(IDictionary<string, object>));
        }

        /// <summary>
        /// Renders a script tag with the specified <paramref name="callback"/> for handling the profile JSON data.
        /// </summary>
        /// <param name="htmlHelper">The HtmlHelper object that renders the script tag.</param>
        /// <param name="emailAddress">The email of the Gravatar profile whose JSON data should be used by the <paramref name="callback"/>.</param>
        /// <param name="callback">A JavaScript function that will be called with the JSON data as a parameter when it is rendered.</param>
        /// <returns>An HTML script tag that can be included in a page.</returns>
        public static MvcHtmlString GravatarProfileScript(this HtmlHelper htmlHelper, string emailAddress, string callback) {
            return MvcHtmlString.Create(GravatarProfileInstance.RenderScript(emailAddress, callback));
        }
    }
}

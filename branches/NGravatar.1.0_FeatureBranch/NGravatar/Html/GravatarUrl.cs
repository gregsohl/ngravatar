using System;
using System.Web.Mvc;

namespace NGravatar.Html {

    /// <summary>
    /// UrlHelper extension methods for retrieving Gravatar avatar images.
    /// </summary>
    public static class GravatarUrl {
    
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
        /// Gets the URI of the Gravatar image for the email address and parameters.
        /// </summary>
        /// <param name="urlHelper">The UrlHelper object getting the URL.</param>
        /// <param name="emailAddress">The email whose Gravatar source should be returned.</param>
        /// <param name="size">The size of the requested Gravatar.</param>
        /// <param name="rating">The maximum Gravatar rating to allow for requested images.</param>
        /// <param name="default">The default image to return if no Gravatar is found for the specified <paramref name="emailAddress"/>.</param>
        /// <param name="forceDefault"><c>true</c> to force the <paramref name="default"/> image to be loaded. Otherwise, <c>false</c>.</param>
        /// <returns>The URI of the Gravatar for the email address and parameters.</returns>
        public static string Gravatar(this UrlHelper urlHelper, string emailAddress, int? size = null, GravatarRating? rating = null, string @default = null, bool? forceDefault = null) {
            return GravatarInstance.GetUrl(emailAddress, size, rating, @default, forceDefault);
        }

        /// <summary>
        /// Gets a link to the profile of the Gravatar account for the given <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="urlHelper">The UrlHelper object getting the link.</param>
        /// <param name="emailAddress">The email whose Gravatar profile link should be returned.</param>
        /// <returns>A link to the Gravatar profile page for the given <paramref name="emailAddress"/>.</returns>
        public static string GravatarProfile(this UrlHelper urlHelper, string emailAddress) {
            return GravatarProfileInstance.GetUrl(emailAddress);
        }
    }
}

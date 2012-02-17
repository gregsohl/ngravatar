using System;
using System.Web.Mvc;

namespace NGravatar.Html
{
    /// <summary>
    /// UrlHelper extension methods for retrieving Gravatar avatar images.
    /// </summary>
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Gets the URI of the Gravatar image for the specifications.
        /// </summary>
        /// <param name="urlHelper">The UrlHelper object getting the URI.</param>
        /// <param name="email">The email whose Gravatar source should be returned.</param>
        /// <param name="size">The size of the requested Gravatar.</param>
        /// <param name="defaultImage">The default image to return if no Gravatar is found for the specified <paramref name="email"/>.</param>
        /// <param name="maxRating">The maximum Gravatar rating to allow for requested images..</param>
        /// <returns>The URI of the Gravatar for the specifications.</returns>
        public static string Gravatar(this UrlHelper urlHelper, string email, int? size, string defaultImage, Rating? maxRating)
        {
            var gravatar = new Gravatar();
            gravatar.DefaultImage = defaultImage;
            if (size.HasValue) gravatar.Size = size.Value;
            if (maxRating.HasValue) gravatar.MaxRating = maxRating.Value;
            
            return gravatar.GetImageSource(email);            
        }

        /// <summary>
        /// Gets the URI of the Gravatar image for the specifications.
        /// </summary>
        /// <param name="urlHelper">The UrlHelper object getting the URI.</param>
        /// <param name="email">The email whose Gravatar source should be returned.</param>
        /// <param name="size">The size of the requested Gravatar.</param>
        /// <returns>The URI of the Gravatar for the specifications.</returns>
        public static string Gravatar(this UrlHelper urlHelper, string email, int? size)
        {
            return Gravatar(urlHelper, email, size, null, null);   
        }
    }
}

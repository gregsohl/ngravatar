using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

using NGravatar.Utils;

namespace NGravatar {

    /// <summary>
    /// Class whose instances can retrieve Gravatar avatar images.
    /// </summary>
    public class Gravatar {

        internal GravatarHasher Hasher {
            get {
                if (null == _Hasher) _Hasher = GravatarHasher.DefaultInstance;
                return _Hasher;
            }
            set {
                if (null == value) throw new ArgumentNullException("Hasher");
                _Hasher = value;
            }
        }
        private GravatarHasher _Hasher;

        internal HtmlBuilder HtmlBuilder {
            get {
                if (null == _HtmlBuilder) _HtmlBuilder = HtmlBuilder.DefaultInstance;
                return _HtmlBuilder;
            }
            set {
                if (null == value) throw new ArgumentNullException("HtmlBuilder");
                _HtmlBuilder = value;
            }
        }
        private HtmlBuilder _HtmlBuilder;

        internal string GetBaseUrl(bool? useHttps) {
            useHttps = useHttps ?? UseHttps;
            return (useHttps.HasValue && useHttps.Value)
                ? "https://secure.gravatar.com"
                : "http://www.gravatar.com";
        }

        /// <summary>
        /// Gets or sets the default instance of <see cref="T:Gravatar"/> used.
        /// </summary>
        /// <exception cref="T:ArgumentNullException">
        /// Thrown if <c>value</c> is <c>null</c>.
        /// </exception>
        public static Gravatar DefaultInstance {
            get {
                if (null == _DefaultInstance) _DefaultInstance = new Gravatar();
                return _DefaultInstance;
            }
            set {
                if (null == value) throw new ArgumentNullException("DefaultInstance");
                _DefaultInstance = value;
            }
        }
        private static Gravatar _DefaultInstance;

        /// <summary>
        /// Gets or sets the maximum Gravatar rating allowed to be displayed
        /// or <c>null</c> to allow the default Gravatar rating.
        /// </summary>
        public GravatarRating? Rating {
            get { return _Rating; }
            set { _Rating = value; }
        }
        private GravatarRating? _Rating;

        /// <summary>
        /// Gets or sets the default size of Gravatar images rendered
        /// as HTML elements.
        /// </summary>
        public int RenderedSize {
            get { return _RenderedSize; }
            set { _RenderedSize = value; }
        }
        private int _RenderedSize = 80;

        /// <summary>
        /// Gets or sets the image size, in pixels, of the Gravatar images
        /// or <c>null</c> to use the default Gravatar size.
        /// </summary>
        public int? Size {
            get { return _Size; }
            set { _Size = value; }
        }
        private int? _Size;

        /// <summary>
        /// Gets or sets the URL of the default image to be shown if no 
        /// Gravatar is found for an email address.
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Gets or sets a flag that indicates whether or not the default image
        /// should be forced for all Gravatar avatar images.
        /// </summary>
        public bool ForceDefault { get; set; }

        /// <summary>
        /// Gets or sets a flag that indicates whether or not Gravatar requests
        /// will use HTTPS.
        /// </summary>
        public bool UseHttps { get; set; }

        /// <summary>
        /// Converts the given <paramref name="emailAddress"/> to the hash needed
        /// to get the associated gravatar image.
        /// </summary>
        /// <param name="emailAddress">The email address for which the hash should be computed.</param>
        /// <returns>The hash of the <paramref name="emailAddress"/>.</returns>
        public string GetHash(string emailAddress) {
            return Hasher.Hash(emailAddress);
        }

        /// <summary>
        /// Gets a link to the image file of the Gravatar for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email whose Gravatar image source should be returned.</param>
        /// <param name="size">The size of the Gravatar image, or <c>null</c> to use the default size.</param>
        /// <param name="rating">The allowed rating of the Gravatar avatar, or <c>null</c> to use the default rating.</param>
        /// <param name="default">The location of the default Gravatar image, or <c>null</c> to use the default location.</param>
        /// <param name="forceDefault"><c>true</c> to force the <paramref name="default"/> image to be loaded. Otherwise, <c>false</c>.</param>
        /// <param name="useHttps"><c>true</c> to use the base HTTPS Gravatar URL. Otherwise, <c>false</c>.</param>
        /// <returns>The URL of the Gravatar for the specified <paramref name="emailAddress"/>.</returns>
        public string GetUrl(string emailAddress, int? size = null, GravatarRating? rating = null, string @default = null, bool? forceDefault = null, bool? useHttps = null) {

            var query = string.Empty;

            size = size ?? Size;
            if (size.HasValue) query += "s=" + size.Value;

            rating = rating ?? Rating;
            if (rating.HasValue) query += "&r=" + rating.Value.ToString().ToLower();

            @default = @default ?? Default;
            if (@default != null) query += "&d=" + HttpUtility.UrlEncode(@default);

            forceDefault = forceDefault ?? ForceDefault;
            if (forceDefault.HasValue && forceDefault.Value) query += "&f=y";

            return string.Format("{0}/avatar/{1}{2}", GetBaseUrl(useHttps), GetHash(emailAddress), 
                query == string.Empty
                    ? string.Empty
                    : "?" + query.TrimStart(new[] { '&' })
            );
        }

        /// <summary>
        /// Creates an img tag whose source is the address of the Gravatar for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="size">The size of the Gravatar image, or <c>null</c> to use the default size.</param>
        /// <param name="rating">The allowed rating of the Gravatar avatar, or <c>null</c> to use the default rating.</param>
        /// <param name="default">The location of the default Gravatar image, or <c>null</c> to use the default location.</param>
        /// <param name="forceDefault"><c>true</c> to force the <paramref name="default"/> image to be loaded. Otherwise, <c>false</c>.</param>        
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <param name="htmlAttributes">Additional attributes to include in the img tag.</param>
        /// <returns>An HTML img tag of the rendered Gravatar.</returns>
        public string Render(string emailAddress, int? size = null, GravatarRating? rating = null, string @default = null, bool? forceDefault = null, bool? useHttps = null, IDictionary<string, object> htmlAttributes = null) {

            size = size ?? Size ?? RenderedSize;

            htmlAttributes = htmlAttributes == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(htmlAttributes);
            htmlAttributes["src"] = GetUrl(emailAddress, size, rating, @default, forceDefault, useHttps);
            htmlAttributes["width"] = size;
            htmlAttributes["height"] = size;

            return HtmlBuilder.RenderImageTag(htmlAttributes);
        }
    }
}

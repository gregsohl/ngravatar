using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace NGravatar {

    /// <summary>
    /// Class whose instances can retrieve Gravatar avatar images.
    /// </summary>
    public class Gravatar {

        private readonly UTF8Encoding Encoding = new UTF8Encoding();
        private readonly MD5CryptoServiceProvider MD5CryptoServiceProvider = new MD5CryptoServiceProvider();

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
        /// Converts the given <paramref name="emailAddress"/> to the hash needed
        /// to get the associated gravatar image.
        /// </summary>
        /// <param name="emailAddress">The email address for which the hash should be computed.</param>
        /// <returns>The hash of the <paramref name="emailAddress"/>.</returns>
        public string GetEmailHash(string emailAddress) {
            if (null == emailAddress) throw new ArgumentNullException("email");

            emailAddress = emailAddress.Trim().ToLower();

            var hashedBytes = MD5CryptoServiceProvider.ComputeHash(Encoding.GetBytes(emailAddress));
            var length = hashedBytes.Length;
            var sb = new StringBuilder(length * 2);

            for (var i = 0; i < length; i++) {
                sb.Append(hashedBytes[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }

        /// <summary>
        /// Gets a link to the image file of the Gravatar for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email whose Gravatar image source should be returned.</param>
        /// <param name="rating">The allowed rating of the Gravatar avatar, or <c>null</c> to use the default rating.</param>
        /// <param name="size">The size of the Gravatar image, or <c>null</c> to use the default size.</param>
        /// <param name="default">The location of the default Gravatar image, or <c>null</c> to use the default location.</param>
        /// <returns>The URL of the Gravatar for the specified <paramref name="emailAddress"/>.</returns>
        public string GetUrl(string emailAddress, GravatarRating? rating = null, int? size = null, string @default = null) {

            var hash = GetEmailHash(emailAddress);
            var url = "http://www.gravatar.com/avatar.php?gravatar_id=" + hash;

            rating = rating ?? Rating;
            if (rating.HasValue) url += "&rating=" + rating.Value;

            size = size ?? Size;
            if (size.HasValue) url += "&size=" + size.Value;

            @default = @default ?? Default;
            if (@default != default(string)) url += "&default=" + HttpUtility.UrlEncode(@default);

            return url;
        }

        /// <summary>
        /// Creates an img tag whose source is the address of the Gravatar for the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address whose Gravatar should be rendered.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the img tag.</param>
        /// <returns>An HTML img tag of the rendered Gravatar.</returns>
        public string Render(string emailAddress, GravatarRating? rating = null, int? size = null, string @default = null, IDictionary<string, string> htmlAttributes = null) {

            var attrs = " ";
            if (htmlAttributes != null) {
                htmlAttributes.Remove("src");
                htmlAttributes.Remove("width");
                htmlAttributes.Remove("height");
                htmlAttributes.ToList().ForEach(pair => {
                    attrs += string.Format("{0}=\"{1}\" ", pair.Key, pair.Value);
                });
            }

            size = size ?? Size ?? RenderedSize;

            return string.Format(
                "<img src=\"{0}\" width=\"{1}\" height=\"{1}\"{2}/>",
                GetUrl(emailAddress, rating, size, @default),
                size,
                attrs
            );
        }
    }
}

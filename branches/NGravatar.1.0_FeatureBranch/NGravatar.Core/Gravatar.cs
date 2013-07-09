using System;
using System.Web;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace NGravatar.Core {
    public class Gravatar {

        private string GetEmailHash(string email) {
            if (null == email) throw new ArgumentNullException("email");

            email = email.Trim().ToLower();

            var encoder = new UTF8Encoding();
            var md5 = new MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(email));
            var sb = new StringBuilder(hashedBytes.Length * 2);

            for (var i = 0; i < hashedBytes.Length; i++) {
                sb.Append(hashedBytes[i].ToString("X2"));
            }

            return sb.ToString().ToLower();
        }

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
        /// Gets a link to the image file of the Gravatar for the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email whose Gravatar image source should be returned.</param>
        /// <returns>The URL of the Gravatar for the specified <paramref name="email"/>.</returns>
        public string GetUrl(string email) {

            var hash = GetEmailHash(email);
            var url = "http://www.gravatar.com/avatar.php?gravatar_id=" + hash;

            var rating = Rating;
            if (rating.HasValue) url += "&rating=" + rating.Value;

            var size = Size;
            if (size.HasValue) url += "&size=" + size.Value;

            var @default = Default;
            if (@default != default(string)) url += "&default=" + HttpUtility.UrlEncode(@default);

            return url;
        }

        /// <summary>
        /// Creates an img tag whose source is the address of the Gravatar for the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email address whose Gravatar should be rendered.</param>
        /// <param name="htmlAttributes">Additional attributes to include in the img tag.</param>
        /// <returns>An HTML img tag of the rendered Gravatar.</returns>
        public string Render(string email, IDictionary<string, string> htmlAttributes) {

            var attrs = "";
            if (htmlAttributes != null) {
                htmlAttributes.Remove("src");
                htmlAttributes.Remove("width");
                htmlAttributes.Remove("height");
                htmlAttributes.ToList().ForEach(pair => {
                    attrs += pair.Key + "=\"" + pair.Value + "\" ";
                });
            }

            var url = GetUrl(email);
            var size = Size;

            var img = "<img " + attrs;
            img += "src=\"" + url + "\" ";
            img += "width=\"" + size + "\" ";
            img += "height=\"" + size + "\" ";
            img += "/>";

            return img;
        }

        /// <summary>
        /// Creates an img tag whose source is the address of the Gravatar for the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email address whose Gravatar should be rendered.</param>
        /// <returns>An HTML img tag of the rendered Gravatar.</returns>
        public string Render(string email) {
            return Render(email, null);
        }
    }
}

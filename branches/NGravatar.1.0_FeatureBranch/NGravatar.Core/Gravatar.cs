using System;
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
        public GravatarRating? MaxRating {
            get { return _MaxRating; }
            set { _MaxRating = value; }
        }
        private GravatarRating? _MaxRating;

        /// <summary>
        /// Gets or sets the image size, in pixels, of the Gravatar images
        /// or <c>null</c> to use the default Gravatar size.
        /// </summary>
        public int? ImageSize {
            get { return _ImageSize; }
            set { _ImageSize = value; }
        }
        private int? _ImageSize;

        /// <summary>
        /// Gets or sets the URL of the default image to be shown if no 
        /// Gravatar is found for an email address.
        /// </summary>
        public string DefaultImage { get; set; }


    }
}

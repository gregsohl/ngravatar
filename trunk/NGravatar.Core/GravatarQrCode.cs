using System;

namespace NGravatar {

    /// <summary>
    /// Client class for the Gravatar QR code API.
    /// </summary>
    public class GravatarQrCode {

        /// <summary>
        /// Gets the URL that links to the Gravatar QR code of the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email whose QR code should be linked.</param>
        /// <param name="size">
        /// The requested size of the QR code.
        /// </param>
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>The URL of the QR code for the specified <paramref name="emailAddress"/>.</returns>
        public string GetUrl(string emailAddress, int? size, bool? useHttps = null) {
            var g = Gravatar.DefaultInstance;
            var url = g.GetBaseUrl(useHttps) + "/" + g.GetHash(emailAddress) + ".qr";

            size = size ?? g.Size;

            return size.HasValue
                ? url + "?s=" + size.Value
                : url;
        }


    }
}

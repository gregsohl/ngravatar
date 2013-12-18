using System;

namespace NGravatar {

    public class GravatarVCard {

        /// <summary>
        /// Gets the URL that links to the Gravatar vCard of the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email whose vCard should be linked.</param>
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>The URL of the vCard for the specified <paramref name="emailAddress"/>.</returns>
        public string GetUrl(string emailAddress, bool? useHttps = null) {
            var g = Gravatar.DefaultInstance;
            var url = g.GetBaseUrl(useHttps) + "/" + g.GetHash(emailAddress) + ".vcf";
            return url;
        }
    }
}

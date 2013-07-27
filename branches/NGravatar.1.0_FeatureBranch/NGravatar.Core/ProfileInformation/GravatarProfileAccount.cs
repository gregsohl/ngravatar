using System;

namespace NGravatar.ProfileInformation {

    /// <summary>
    /// A field in a Gravatar profile that provides information about a user's account.
    /// </summary>
    public class GravatarProfileAccount : GravatarProfilePluralField {

        /// <summary>
        /// Gets the domain of this account.
        /// </summary>
        public string Domain { get { return _Domain; } }
        private readonly string _Domain;

        /// <summary>
        /// Gets the username of this account.
        /// </summary>
        public string Username { get { return _Username; } }
        private readonly string _Username;

        /// <summary>
        /// Gets a display string for this account.
        /// </summary>
        public string Display { get { return _Display; } }
        private readonly string _Display;

        /// <summary>
        /// Gets the URL of this account.
        /// </summary>
        public string Url { get { return _Url; } }
        private readonly string _Url;

        /// <summary>
        /// Gets the short name of this account.
        /// </summary>
        public string Shortname { get { return _Shortname; } }
        private readonly string _Shortname;

        /// <summary>
        /// Gets a flag indicating whether or not this account has been verified.
        /// </summary>
        public bool Verified { get { return _Verified; } }
        private readonly bool _Verified;

        /// <summary>
        /// Creates a new instance of a Gravatar profile account.
        /// </summary>
        /// <param name="domain">The domain of the account.</param>
        /// <param name="username">The account username.</param>
        /// <param name="display">The string to display for the account.</param>
        /// <param name="url">The URL of the account.</param>
        /// <param name="shortname">The short name of the account.</param>
        /// <param name="verified">A flag indicating whether or not the account is verified.</param>
        public GravatarProfileAccount(string domain, string username, string display, string url, string shortname, bool verified) {
            _Url = url;
            _Domain = domain;
            _Display = display;
            _Verified = verified;
            _Username = username;
            _Shortname = shortname;
        }

        /// <summary>
        /// Gets a string representation of the account.
        /// </summary>
        /// <returns>The account string to display.</returns>
        public override string ToString() {
            return Display;
        }
    }
}

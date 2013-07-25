using System;

namespace NGravatar {

    /// <summary>
    /// A URL field in a Gravatar profile.
    /// </summary>
    public class GravatarProfileUrl : GravatarProfilePluralField {

        /// <summary>
        /// Gest the title of the URL.
        /// </summary>
        public string Title { get { return _Title; } }
        private readonly string _Title;

        /// <summary>
        /// Creates a new instance of the URL Gravatar profile field.
        /// </summary>
        /// <param name="title">The title of the URL.</param>
        /// <param name="value">The URL value.</param>
        public GravatarProfileUrl(string title, string value) : base(value) {
            _Title = title;
        }
    }
}

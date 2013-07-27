using System;

namespace NGravatar.ProfileInformation {

    /// <summary>
    /// An email field in a Gravatar profile.
    /// </summary>
    public class GravatarProfileEmail : GravatarProfilePluralField {

        /// <summary>
        /// Creates a new instance of the email field.
        /// </summary>
        /// <param name="value">The email address.</param>
        public GravatarProfileEmail(string value) : base(value, false) { }

        /// <summary>
        /// Creates a new instance of the email field.
        /// </summary>
        /// <param name="value">The email address.</param>
        /// <param name="primary">A flag indicating whether or not this is the primary email address of the profile.</param>
        public GravatarProfileEmail(string value, bool primary) : base(value, primary) { }
    }
}

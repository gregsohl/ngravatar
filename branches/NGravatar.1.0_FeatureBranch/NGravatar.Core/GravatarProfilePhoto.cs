using System;

namespace NGravatar {
    
    /// <summary>
    /// A photo field in a Gravatar profile.
    /// </summary>
    public class GravatarProfilePhoto : GravatarProfilePluralField {

        /// <summary>
        /// Creates a new instance of the photo field
        /// </summary>
        /// <param name="value">The URI of the photo.</param>
        public GravatarProfilePhoto(string value) : this(value, null) { }

        /// <summary>
        /// Creates a new instance of the photo field.
        /// </summary>
        /// <param name="value">The URI of the photo.</param>
        /// <param name="type">The type of the photo.</param>
        public GravatarProfilePhoto(string value, string type) : base(value, type) { }
    }
}

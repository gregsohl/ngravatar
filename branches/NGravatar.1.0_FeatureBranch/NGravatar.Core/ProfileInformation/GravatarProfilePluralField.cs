using System;

namespace NGravatar.ProfileInformation {

    /// <summary>
    /// A field of a Gravatar profile that may have more than one instance per profile.
    /// </summary>
    public class GravatarProfilePluralField {

        /// <summary>
        /// Gets the value of the field.
        /// </summary>
        public string Value { get { return _Value; } }
        private readonly string _Value;

        /// <summary>
        /// Gets the type of the field.
        /// </summary>
        public string Type { get { return _Type; } }
        private readonly string _Type;

        /// <summary>
        /// Gets a flag indicating whether or not this field is the primary instance
        /// of all other similar fields.
        /// </summary>
        public bool Primary { get { return _Primary; } }
        private readonly bool _Primary;

        /// <summary>
        /// Creates a new instance of the field.
        /// </summary>
        public GravatarProfilePluralField() : this(null) { }

        /// <summary>
        /// Creates a new instance of the field.
        /// </summary>
        /// <param name="value">The value of the field.</param>
        public GravatarProfilePluralField(string value) : this(value, null) { }

        /// <summary>
        /// Creates a new instance of the Gravatar profile field.
        /// </summary>
        /// <param name="value">The value of the field.</param>
        /// <param name="type">The type of the field.</param>
        public GravatarProfilePluralField(string value, string type) : this(value, type, false) { }

        /// <summary>
        /// Creates a new instance of the Gravatar profile field.
        /// </summary>
        /// <param name="value">The value of the field.</param>
        /// <param name="primary">A flag indicating whether or not this field is the primary instance of all other similar fields.</param>
        public GravatarProfilePluralField(string value, bool primary) : this(value, null, primary) { }

        /// <summary>
        /// Creates a new instance of the Gravatar profile field.
        /// </summary>
        /// <param name="value">The value of the field.</param>
        /// <param name="type">The type of the field.</param>
        /// <param name="primary">A flag indicating whether or not this field is the primary instance of all other similar fields.</param>
        public GravatarProfilePluralField(string value, string type, bool primary) {
            _Type = type;
            _Value = value;
            _Primary = primary;
        }

        /// <summary>
        /// Gets a string representation of the field.
        /// </summary>
        /// <returns>The field value as a string.</returns>
        public override string ToString() {
            return Value;
        }
    }
}

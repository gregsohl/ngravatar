using System;

namespace NGravatar {

    /// <summary>
    /// The name field of a Gravatar profile.
    /// </summary>
    public class GravatarProfileName {

        /// <summary>
        /// Gets the full name, including all middle names, titles, and suffixes as appropriate, formatted for display (e.g. Mr. Joseph Robert Smarr, Esq.). This is the Primary Sub-Field for this field, for the purposes of sorting and filtering. 
        /// </summary>
        public string Formatted { get { return _Formatted; } }
        private readonly string _Formatted;

        /// <summary>
        /// Gets the family name of this Contact, or "Last Name" in most Western languages (e.g. Smarr given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string FamilyName { get { return _FamilyName; } }
        private readonly string _FamilyName;

        /// <summary>
        /// Gets the given name of this Contact, or "First Name" in most Western languages (e.g. Joseph given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string GivenName { get { return _GivenName; } }
        private readonly string _GivenName;

        /// <summary>
        /// Gets the middle name(s) of this Contact (e.g. Robert given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string MiddleName { get { return _MiddleName; } }
        private readonly string _MiddleName;

        /// <summary>
        /// Gets the honorific prefix(es) of this Contact, or "Title" in most Western languages (e.g. Mr. given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string HonorificPrefix { get { return _HonorificPrefix; } }
        private readonly string _HonorificPrefix;

        /// <summary>
        /// Gets the honorifix suffix(es) of this Contact, or "Suffix" in most Western languages (e.g. Esq. given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string HonorificSuffix { get { return _HonorificSuffix; } }
        private readonly string _HonorificSuffix;

        /// <summary>
        /// Creates a new instance of the Gravatar profile name.
        /// </summary>
        /// <param name="formatted">The full formatted name.</param>
        /// <param name="familyName">The family name or surname.</param>
        /// <param name="givenName">The given name or first name.</param>
        /// <param name="middleName">The middle name.</param>
        /// <param name="honorificPrefix">The prefix of the name.</param>
        /// <param name="honorificSuffix">The suffix of the name.</param>
        public GravatarProfileName(string formatted, string familyName, string givenName, string middleName, string honorificPrefix, string honorificSuffix) {
            _Formatted = formatted;
            _GivenName = givenName;
            _MiddleName = middleName;
            _FamilyName = familyName;
            _HonorificPrefix = honorificPrefix;
            _HonorificSuffix = honorificSuffix;
        }

        /// <summary>
        /// Gets the name formatted as a string.
        /// </summary>
        /// <returns>The string formatted name.</returns>
        public override string ToString() {
            return Formatted;
        }
    }
}

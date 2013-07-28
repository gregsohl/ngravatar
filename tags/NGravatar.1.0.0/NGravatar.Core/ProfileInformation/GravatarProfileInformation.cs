using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;

namespace NGravatar.ProfileInformation {

    /// <summary>
    /// Gravatar profile information object.
    /// </summary>
    public class GravatarProfileInformation {

        internal GravatarProfileParser Parser {
            get {
                if (null == _Parser) _Parser = new GravatarProfileParser();
                return _Parser;
            }
            set {
                if (null == value) throw new ArgumentNullException("Parser");
                _Parser = value;
            }
        }
        private GravatarProfileParser _Parser;

        /// <summary>
        /// Gets the ID value of this profile.
        /// </summary>
        public string Id { 
            get { return _Id ?? (_Id = Parser.ParseId()); }
        }
        private string _Id;

        /// <summary>
        /// Gets a flag that indicates whether or not this profile exists in
        /// Gravatar's system.
        /// </summary>
        public bool Exists {
            get { return (_Exists ?? (_Exists = Parser.EntryExists())).Value; }
        }
        private bool? _Exists;

        /// <summary>
        /// Gets this profile's hash.
        /// </summary>
        public string Hash { get { return _Hash ?? (_Hash = Parser.ParseHash()); } }
        private string _Hash;

        /// <summary>
        /// Gets this profile's request hash.
        /// </summary>
        public string RequestHash { get { return _RequestHash ?? (_RequestHash = Parser.ParseRequestHash()); } }
        private string _RequestHash;

        /// <summary>
        /// Gets the URL of this profile.
        /// </summary>
        public string ProfileUrl { get { return _ProfileUrl ?? (_ProfileUrl = Parser.ParseProfileUrl()); } }
        private string _ProfileUrl;

        /// <summary>
        /// Gets the preferred username of this profile's owner.
        /// </summary>
        public string PreferredUsername { get { return _PreferredUsername ?? (_PreferredUsername = Parser.ParsePreferredUsername()); } }
        private string _PreferredUsername;

        /// <summary>
        /// Gets the URL of the thumbnail for this profile.
        /// </summary>
        public string ThumbnailUrl { get { return _ThumbnailUrl ?? (_ThumbnailUrl = Parser.ParseThumbnailUrl()); } }
        private string _ThumbnailUrl;

        /// <summary>
        /// Gets the name to display for the owner of this profile.
        /// </summary>
        public string DisplayName { get { return _DisplayName ?? (_DisplayName = Parser.ParseDisplayName()); } }
        private string _DisplayName;

        /// <summary>
        /// Gets information about the owner of this profile.
        /// </summary>
        public string AboutMe { get { return _AboutMe ?? (_AboutMe = Parser.ParseAboutMe()); } }
        private string _AboutMe;

        /// <summary>
        /// Gets the current location value of this profile.
        /// </summary>
        public string CurrentLocation { get { return _CurrentLocation ?? (_CurrentLocation = Parser.ParseCurrentLocation()); } }
        private string _CurrentLocation;

        /// <summary>
        /// Gets the profile name section of this profile.
        /// </summary>
        public GravatarProfileName Name { get { return _Name ?? (_Name = Parser.ParseName()); } }
        private GravatarProfileName _Name;

        /// <summary>
        /// Gets a collection of URLs associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfileUrl> Urls { get { return _Urls ?? (_Urls = Parser.ParseUrls().ToList()); } }
        private IEnumerable<GravatarProfileUrl> _Urls;

        /// <summary>
        /// Gets a collection of emails associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfileEmail> Emails { get { return _Emails ?? (_Emails = Parser.ParseEmails().ToList()); } }
        private IEnumerable<GravatarProfileEmail> _Emails;

        /// <summary>
        /// Gets a collection of photos associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfilePhoto> Photos { get { return _Photos ?? (_Photos = Parser.ParsePhotos().ToList()); } }
        private IEnumerable<GravatarProfilePhoto> _Photos;

        /// <summary>
        /// Gets a collection of accounts associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfileAccount> Accounts { get { return _Accounts ?? (_Accounts = Parser.ParseAccounts().ToList()); } }
        private IEnumerable<GravatarProfileAccount> _Accounts;

        /// <summary>
        /// Returns the <see cref="GravatarProfileInformation.DisplayName"/>.
        /// </summary>
        /// <returns>The <see cref="GravatarProfileInformation.DisplayName"/>.</returns>
        public override string ToString() {
            return DisplayName;
        }
    }
}

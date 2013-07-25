using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;

namespace NGravatar {

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
        /// Gets this profile hash.
        /// </summary>
        public string Hash { get { return _Hash ?? (_Hash = Parser.ParseHash()); } }
        private string _Hash;

        /// <summary>
        /// Gets this profile request hash.
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
        public IEnumerable<GravatarProfileUrl> Urls { get { return _Urls ?? (_Urls = Parser.ParseUrls()); } }
        private IEnumerable<GravatarProfileUrl> _Urls;

        /// <summary>
        /// Gets a collection of emails associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfileEmail> Emails { get { return _Emails ?? (_Emails = Parser.ParseEmails()); } }
        private IEnumerable<GravatarProfileEmail> _Emails;

        /// <summary>
        /// Gets a collection of photos associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfilePhoto> Photos { get { return _Photos ?? (_Photos = Parser.ParsePhotos()); } }
        private IEnumerable<GravatarProfilePhoto> _Photos;

        /// <summary>
        /// Gets a collection of accounts associated with this profile.
        /// </summary>
        public IEnumerable<GravatarProfileAccount> Accounts { get { return _Accounts ?? (_Accounts = Parser.ParseAccounts()); } }
        private IEnumerable<GravatarProfileAccount> _Accounts;

        public override string ToString() {
            return DisplayName;
        }
    }





//
//
//    /// <summary>
//    /// Interface for Gravatar profile information objects.
//    /// </summary>
//    public interface IGrofileInfo {
//
//        /// <summary>
//        /// Gets the ID value of this profile.
//        /// </summary>
//        string Id { get; }
//
//        /// <summary>
//        /// Gets this profile hash.
//        /// </summary>
//        string Hash { get; }
//
//        /// <summary>
//        /// Gets this profile request hash.
//        /// </summary>
//        string RequestHash { get; }
//
//        /// <summary>
//        /// Gets the URL of this profile.
//        /// </summary>
//        string ProfileUrl { get; }
//
//        /// <summary>
//        /// Gets the preferred username of this profile's owner.
//        /// </summary>
//        string PreferredUsername { get; }
//
//        /// <summary>
//        /// Gets the URL of the thumbnail for this profile.
//        /// </summary>
//        string ThumbnailUrl { get; }
//
//        /// <summary>
//        /// Gets the name to display for the owner of this profile.
//        /// </summary>
//        string DisplayName { get; }
//
//        /// <summary>
//        /// Gets information about the owner of this profile.
//        /// </summary>
//        string AboutMe { get; }
//
//        /// <summary>
//        /// Gets the current location value of this profile.
//        /// </summary>
//        string CurrentLocation { get; }
//
//        /// <summary>
//        /// Gets the profile name section of this profile.
//        /// </summary>
//        GrofileName Name { get; }
//
//        /// <summary>
//        /// Gets a collection of URLs associated with this profile.
//        /// </summary>
//        IEnumerable<GrofileUrl> Urls { get; }
//
//        /// <summary>
//        /// Gets a collection of emails associated with this profile.
//        /// </summary>
//        IEnumerable<GrofileEmail> Emails { get; }
//
//        /// <summary>
//        /// Gets a collection of photos associated with this profile.
//        /// </summary>
//        IEnumerable<GrofilePhoto> Photos { get; }
//
//        /// <summary>
//        /// Gets a collection of accounts associated with this profile.
//        /// </summary>
//        IEnumerable<GrofileAccount> Accounts { get; }
//    }
//
//    internal class GrofileInfoXml : IGrofileInfo {
//
//        private readonly XElement _Entry;
//
//        private GrofileName GetName() {
//            var formatted = Entry.ElementValueOrDefault("formatted", null);
//            var familyName = Entry.ElementValueOrDefault("familyName", null);
//            var givenName = Entry.ElementValueOrDefault("givenName", null);
//            var middleName = Entry.ElementValueOrDefault("middleName", null);
//            var honorificPrefix = Entry.ElementValueOrDefault("honorificPrefix", null);
//            var honorificSuffix = Entry.ElementValueOrDefault("honorificSuffix", null);
//
//            return new GrofileName(formatted, familyName, givenName, middleName, honorificPrefix, honorificSuffix);
//        }
//
//        private IEnumerable<GrofileUrl> GetUrls() {
//            var list = new List<GrofileUrl>();
//            var elements = Entry.Elements("urls");
//            foreach (var element in elements) {
//                var title = element.ElementValueOrDefault("title", null);
//                var value = element.ElementValueOrDefault("value", null);
//                list.Add(new GrofileUrl(title, value));
//            }
//            return list.AsEnumerable();
//        }
//
//        private IEnumerable<GrofileEmail> GetEmails() {
//            var list = new List<GrofileEmail>();
//            var elements = Entry.Elements("emails");
//            foreach (var element in elements) {
//                var value = element.ElementValueOrDefault("value", null);
//                var primary = element.ElementValueOrDefault("primary", "false");
//                var primaryValue = false;
//                bool.TryParse(primary, out primaryValue);
//                list.Add(new GrofileEmail(value, primaryValue));
//            }
//            return list.AsEnumerable();
//        }
//
//        private IEnumerable<GrofileAccount> GetAccounts() {
//            var list = new List<GrofileAccount>();
//            var elements = Entry.Elements("accounts");
//            foreach (var element in elements) {
//                var domain = element.ElementValueOrDefault("domain", null);
//                var username = element.ElementValueOrDefault("username", null);
//                var display = element.ElementValueOrDefault("display", null);
//                var url = element.ElementValueOrDefault("url", null);
//                var shortname = element.ElementValueOrDefault("shortname", null);
//                var verified = element.ElementValueOrDefault("verified", "false");
//                var verifiedValue = false;
//                bool.TryParse(verified, out verifiedValue);
//                list.Add(new GrofileAccount(domain, username, display, url, shortname, verifiedValue));
//            }
//            return list.AsEnumerable();
//        }
//
//        private IEnumerable<GrofilePhoto> GetPhotos() {
//            var list = new List<GrofilePhoto>();
//            var elements = Entry.Elements("photos");
//            foreach (var element in elements) {
//                var value = element.ElementValueOrDefault("value", null);
//                var type = element.ElementValueOrDefault("type", null);
//                list.Add(new GrofilePhoto(value, type));
//            }
//            return list.AsEnumerable();
//        }
//
//        public XElement Entry { get { return _Entry; } }
//
//        public string Id { get; private set; }
//        public string Hash { get; private set; }
//        public string RequestHash { get; private set; }
//        public string ProfileUrl { get; private set; }
//        public string PreferredUsername { get; private set; }
//        public string ThumbnailUrl { get; private set; }
//        public string DisplayName { get; private set; }
//        public string AboutMe { get; private set; }
//        public string CurrentLocation { get; private set; }
//
//        public GrofileName Name { get; private set; }
//        public IEnumerable<GrofileUrl> Urls { get; private set; }
//        public IEnumerable<GrofileEmail> Emails { get; private set; }
//        public IEnumerable<GrofilePhoto> Photos { get; private set; }
//        public IEnumerable<GrofileAccount> Accounts { get; private set; }
//
//        public GrofileInfoXml(XElement entry) {
//            if (null == entry) throw new ArgumentNullException("entry");
//            _Entry = entry;
//
//            Id = Entry.ElementValueOrDefault("id", null);
//            Hash = Entry.ElementValueOrDefault("hash", null);
//            RequestHash = Entry.ElementValueOrDefault("requestHash", null);
//            ProfileUrl = Entry.ElementValueOrDefault("profileUrl", null);
//            PreferredUsername = Entry.ElementValueOrDefault("preferredUsername", null);
//            ThumbnailUrl = Entry.ElementValueOrDefault("thumbnailUrl", null);
//            DisplayName = Entry.ElementValueOrDefault("displayName", null);
//            AboutMe = Entry.ElementValueOrDefault("aboutMe", null);
//            CurrentLocation = Entry.ElementValueOrDefault("currentLocation", null);
//
//            Name = GetName();
//            Urls = GetUrls();
//            Emails = GetEmails();
//            Photos = GetPhotos();
//            Accounts = GetAccounts();
//        }
//
//        public override string ToString() {
//            return DisplayName;
//        }
//    }
}

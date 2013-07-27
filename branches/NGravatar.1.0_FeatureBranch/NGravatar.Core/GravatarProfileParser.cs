using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

namespace NGravatar {

    internal class GravatarProfileParser {
      
        private static string GetValueOrDefault(XElement element, string elementName, string defaultValue) {
            if (null == element) throw new ArgumentNullException("element");
            var el = element.Element(elementName);
            if (el == null) return defaultValue;
            return el.Value;
        }

        private static bool GetBooleanOrDefault(XElement element, string elementName, bool defaultValue) {
            var value = GetValueOrDefault(element, elementName);
            var result = default(bool);
            return bool.TryParse(value, out result)
                ? result
                : defaultValue;
        }

        private static string GetValueOrDefault(XElement element, string elementName) {
            return GetValueOrDefault(element, elementName, null);
        }

        private static bool GetBooleanOrDefault(XElement element, string elementName) {
            return GetBooleanOrDefault(element, elementName, false);
        }

        public XElement Entry {
            get {
                if (null == _Entry) _Entry = new XElement("entry");
                return _Entry;
            }
            set {
                if (null == value) throw new ArgumentNullException("Entry");
                _Entry = value;
            }
        }
        private XElement _Entry;

        public virtual bool EntryExists() {
            return ParseId() != null;
        }

        public virtual string ParseId() {
            return GetValueOrDefault(Entry, "id");
        }

        public virtual string ParseHash() {
            return GetValueOrDefault(Entry, "hash");
        }

        public virtual string ParseRequestHash() {
            return GetValueOrDefault(Entry, "requestHash");
        }

        public virtual string ParseProfileUrl() {
            return GetValueOrDefault(Entry, "profileUrl");
        }

        public virtual string ParsePreferredUsername() {
            return GetValueOrDefault(Entry, "preferredUsername");
        }

        public virtual string ParseThumbnailUrl() {
            return GetValueOrDefault(Entry, "thumbnailUrl");
        }

        public virtual string ParseDisplayName() {
            return GetValueOrDefault(Entry, "displayName");
        }

        public virtual string ParseAboutMe() {
            return GetValueOrDefault(Entry, "aboutMe");
        }

        public virtual string ParseCurrentLocation() {
            return GetValueOrDefault(Entry, "currentLocation");
        }

        public virtual GravatarProfileName ParseName() {
            var nameElement = Entry.Elements("name").FirstOrDefault();
            if (nameElement == null) return null;

            var formatted = GetValueOrDefault(nameElement, "formatted");
            var givenName = GetValueOrDefault(nameElement, "givenName");
            var middleName = GetValueOrDefault(nameElement, "middleName");
            var familyName = GetValueOrDefault(nameElement, "familyName");
            var honorificPrefix = GetValueOrDefault(nameElement, "honorificPrefix");
            var honorificSuffix = GetValueOrDefault(nameElement, "honorificSuffix");

            return new GravatarProfileName(formatted, familyName, givenName, middleName, honorificPrefix, honorificSuffix);
        }

        public virtual IEnumerable<GravatarProfileUrl> ParseUrls() {
            return Entry
                .Elements("urls")
                .Select(element => 
                    new GravatarProfileUrl(
                        GetValueOrDefault(element, "title"),
                        GetValueOrDefault(element, "value")
                    )
                );
        }

        public virtual IEnumerable<GravatarProfileEmail> ParseEmails() {
            return Entry
                .Elements("emails")
                .Select(element => 
                    new GravatarProfileEmail(
                        GetValueOrDefault(element, "value"),
                        GetBooleanOrDefault(element, "primary")
                    )
                );
        }

        public virtual IEnumerable<GravatarProfilePhoto> ParsePhotos() {
            return Entry
                .Elements("photos")
                .Select(element =>
                    new GravatarProfilePhoto(
                        GetValueOrDefault(element, "value"),
                        GetValueOrDefault(element, "type")
                    )
                );
        }

        public virtual IEnumerable<GravatarProfileAccount> ParseAccounts() {
            return Entry
                .Elements("accounts")
                .Select(element =>
                    new GravatarProfileAccount(
                        GetValueOrDefault(element, "domain"),
                        GetValueOrDefault(element, "username"),
                        GetValueOrDefault(element, "display"),
                        GetValueOrDefault(element, "url"),
                        GetValueOrDefault(element, "shortname"),
                        GetBooleanOrDefault(element, "verified")
                    )
                );
        }
    }
}

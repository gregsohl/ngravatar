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

        private static string GetValueOrDefault(XElement element, string elementName) {
            return GetValueOrDefault(element, elementName, default(string));
        }

        private static IEnumerable<T> EnumerateElements<T>(XElement element, string elementName, Func<XElement, T> selector) {
            if (null == element) throw new ArgumentNullException("element");
            if (null == selector) throw new ArgumentNullException("selector");
            return element
                .Elements(elementName)
                .Select(el => selector(el))
                .ToList();
        }

        private string GetValueOrDefault(string elementName, string defaultValue) {
            return GetValueOrDefault(Entry, elementName, defaultValue);
        }

        private string GetValueOrDefault(string elementName) {
            return GetValueOrDefault(Entry, elementName);
        }

        private IEnumerable<T> EnumerateElements<T>(string elementName, Func<XElement, T> selector) {
            return EnumerateElements(Entry, elementName, selector);
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

        public string ParseId() {
            return GetValueOrDefault("id");
        }

        public string ParseHash() {
            return GetValueOrDefault("hash");
        }

        public string ParseRequestHash() {
            return GetValueOrDefault("requestHash");
        }

        public string ParseProfileUrl() {
            return GetValueOrDefault("profileUrl");
        }

        public string ParsePreferredUsername() {
            return GetValueOrDefault("preferredUsername");
        }

        public string ParseThumbnailUrl() {
            return GetValueOrDefault("thumbnailUrl");
        }

        public string ParseDisplayName() {
            return GetValueOrDefault("displayName");
        }

        public string ParseAboutMe() {
            return GetValueOrDefault("aboutMe");
        }

        public string ParseCurrentLocation() {
            return GetValueOrDefault("currentLocation");
        }

        public GravatarProfileName ParseName() {
            var formatted = GetValueOrDefault("formatted");
            var givenName = GetValueOrDefault("givenName");
            var middleName = GetValueOrDefault("middleName");
            var familyName = GetValueOrDefault("familyName");
            var honorificPrefix = GetValueOrDefault("honorificPrefix");
            var honorificSuffix = GetValueOrDefault("honorificSuffix");

            return new GravatarProfileName(formatted, familyName, givenName, middleName, honorificPrefix, honorificSuffix);
        }

        public IEnumerable<GravatarProfileUrl> ParseUrls() {
            throw new NotImplementedException();
        }

        public IEnumerable<GravatarProfileEmail> ParseEmails() {
            throw new NotImplementedException();
        }

        public IEnumerable<GravatarProfilePhoto> ParsePhotos() {
            throw new NotImplementedException();
        }

        public IEnumerable<GravatarProfileAccount> ParseAccounts() {
            throw new NotImplementedException();
        }
    }
}

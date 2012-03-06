using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NGravatar
{
    static class XElementExtensions
    {
        public static string ElementValueOrDefault(this XElement xElement, string elementName, string defaultValue)
        {
            if (null == xElement) throw new ArgumentNullException("xElement");
            
            var element = xElement.Element(elementName);
            if (element == null) return defaultValue;
            return element.Value;
        }
    }

    public class GrofilePluralField
    {
        public string Value { get; private set; }
        public string Type { get; private set; }
        public bool Primary { get; private set; }

        public GrofilePluralField() : this(null) { }
        public GrofilePluralField(string value) : this(value, null) { }
        public GrofilePluralField(string value, string type) : this(value, type, false) { }
        public GrofilePluralField(string value, bool primary) : this(value, null, primary) { }

        public GrofilePluralField(string value, string type, bool primary)
        {
            Value = value;
            Type = type;
            Primary = primary;
        }
    }

    public class GrofileUrl : GrofilePluralField
    {
        public string Title { get; private set; }

        public GrofileUrl(string title, string value) : base(value)
        {
            Title = title;
        }
    }

    public class GrofileEmail : GrofilePluralField
    {
        public GrofileEmail(string value) : base(value, false) { }
        public GrofileEmail(string value, bool primary) : base(value, primary) { }
    }

    public class GrofilePhoto : GrofilePluralField
    {
        public GrofilePhoto(string value) : this(value, null) { }
        public GrofilePhoto(string value, string type) : base(value, type) { }
    }

    public class GrofileAccount : GrofilePluralField
    {
        public string Domain { get; private set; }
        public string Username { get; private set; }
        public string Display { get; private set; }
        public string Url { get; private set; }
        public string Shortname { get; private set; }
        public bool Verified { get; private set; }

        public GrofileAccount(string domain, string username, string display, string url, string shortname, bool verified)
        {
            Domain = domain;
            Username = username;
            Display = display;
            Url = url;
            Shortname = shortname;
            Verified = verified;
        }
    }

    public class GrofileName
    {
        /// <summary>
        /// Gets the full name, including all middle names, titles, and suffixes as appropriate, formatted for display (e.g. Mr. Joseph Robert Smarr, Esq.). This is the Primary Sub-Field for this field, for the purposes of sorting and filtering. 
        /// </summary>
        public string Formatted { get; private set; }

        /// <summary>
        /// Gets the family name of this Contact, or "Last Name" in most Western languages (e.g. Smarr given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string FamilyName { get; private set; }

        /// <summary>
        /// Gets the given name of this Contact, or "First Name" in most Western languages (e.g. Joseph given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string GivenName { get; private set; }

        /// <summary>
        /// Gets the middle name(s) of this Contact (e.g. Robert given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string MiddleName { get; private set; }

        /// <summary>
        /// Gets the honorific prefix(es) of this Contact, or "Title" in most Western languages (e.g. Mr. given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string HonorificPrefix { get; private set; }

        /// <summary>
        /// Gets the honorifix suffix(es) of this Contact, or "Suffix" in most Western languages (e.g. Esq. given the full name Mr. Joseph Robert Smarr, Esq.). 
        /// </summary>
        public string HonorificSuffix { get; private set; }

        public GrofileName(string formatted, string familyName, string givenName, string middleName, string honorificPrefix, string honorificSuffix)
        {
            Formatted = formatted;
            FamilyName = familyName;
            GivenName = givenName;
            MiddleName = middleName;
            HonorificPrefix = honorificPrefix;
            HonorificSuffix = honorificSuffix;
        }

        public override string ToString()
        {
            return Formatted;
        }
    }

    public interface IGrofileInfo
    {
        string Id { get; }
        string Hash { get; }
        string RequestHash { get; }
        string ProfileUrl { get; }
        string PreferredUsername { get; }
        string ThumbnailUrl { get; }
        string DisplayName { get; }
        string AboutMe { get; }
        string CurrentLocation { get; }

        GrofileName Name { get; }
        IEnumerable<GrofileUrl> Urls { get; }
        IEnumerable<GrofileEmail> Emails { get; }
        IEnumerable<GrofilePhoto> Photos { get; }
        IEnumerable<GrofileAccount> Accounts { get; }
        
    }

    internal class GrofileInfoXml : IGrofileInfo
    {
        private readonly XElement _Entry;

        private GrofileName GetName()
        {
            var formatted = Entry.ElementValueOrDefault("formatted", null);
            var familyName = Entry.ElementValueOrDefault("familyName", null);
            var givenName = Entry.ElementValueOrDefault("givenName", null);
            var middleName = Entry.ElementValueOrDefault("middleName", null);
            var honorificPrefix = Entry.ElementValueOrDefault("honorificPrefix", null);
            var honorificSuffix = Entry.ElementValueOrDefault("honorificSuffix", null);

            return new GrofileName(formatted, familyName, givenName, middleName, honorificPrefix, honorificSuffix);
        }

        private IEnumerable<GrofileUrl> GetUrls()
        {
            var list = new List<GrofileUrl>();
            var elements = Entry.Elements("urls");
            foreach (var element in elements)
            {
                var title = element.ElementValueOrDefault("title", null);
                var value = element.ElementValueOrDefault("value", null);
                list.Add(new GrofileUrl(title, value));
            }
            return list.AsEnumerable();
        }

        private IEnumerable<GrofileEmail> GetEmails()
        {
            var list = new List<GrofileEmail>();
            var elements = Entry.Elements("emails");
            foreach (var element in elements)
            {
                var value = element.ElementValueOrDefault("value", null);
                var primary = element.ElementValueOrDefault("primary", "false");
                var primaryValue = false;
                bool.TryParse(primary, out primaryValue);
                list.Add(new GrofileEmail(value, primaryValue));
            }
            return list.AsEnumerable();
        }

        private IEnumerable<GrofileAccount> GetAccounts()
        {
            var list = new List<GrofileAccount>();
            var elements = Entry.Elements("accounts");
            foreach (var element in elements)
            {
                var domain = element.ElementValueOrDefault("domain", null);
                var username = element.ElementValueOrDefault("username", null);
                var display = element.ElementValueOrDefault("display", null);
                var url = element.ElementValueOrDefault("url", null);
                var shortname = element.ElementValueOrDefault("shortname", null);
                var verified = element.ElementValueOrDefault("verified", "false");
                var verifiedValue = false;
                bool.TryParse(verified, out verifiedValue);
                list.Add(new GrofileAccount(domain, username, display, url, shortname, verifiedValue));                
            }
            return list.AsEnumerable();
        }

        private IEnumerable<GrofilePhoto> GetPhotos()
        {
            var list = new List<GrofilePhoto>();
            var elements = Entry.Elements("photos");
            foreach (var element in elements)
            {
                var value = element.ElementValueOrDefault("value", null);
                var type = element.ElementValueOrDefault("type", null);
                list.Add(new GrofilePhoto(value, type));
            }
            return list.AsEnumerable();
        }

        public XElement Entry { get { return _Entry; } }

        public string Id { get; private set; }
        public string Hash { get; private set; }
        public string RequestHash { get; private set; }
        public string ProfileUrl { get; private set; }
        public string PreferredUsername { get; private set; }
        public string ThumbnailUrl { get; private set; }
        public string DisplayName { get; private set; }
        public string AboutMe { get; private set; }
        public string CurrentLocation { get; private set; }

        public GrofileName Name { get; private set; }
        public IEnumerable<GrofileUrl> Urls { get; private set; }
        public IEnumerable<GrofileEmail> Emails { get; private set; }
        public IEnumerable<GrofilePhoto> Photos { get; private set; }
        public IEnumerable<GrofileAccount> Accounts { get; private set; }

        public GrofileInfoXml(XElement entry)
        {
            if (null == entry) throw new ArgumentNullException("entry");
            _Entry = entry;

            Id = Entry.ElementValueOrDefault("id", null);
            Hash = Entry.ElementValueOrDefault("hash", null);
            RequestHash = Entry.ElementValueOrDefault("requestHash", null);
            ProfileUrl = Entry.ElementValueOrDefault("profileUrl", null);
            PreferredUsername = Entry.ElementValueOrDefault("preferredUsername", null);
            ThumbnailUrl = Entry.ElementValueOrDefault("thumbnailUrl", null);
            DisplayName = Entry.ElementValueOrDefault("displayName", null);
            AboutMe = Entry.ElementValueOrDefault("aboutMe", null);
            CurrentLocation = Entry.ElementValueOrDefault("currentLocation", null);

            Name = GetName();
            Urls = GetUrls();
            Emails = GetEmails();
            Photos = GetPhotos();
            Accounts = GetAccounts();
        }
    }
}

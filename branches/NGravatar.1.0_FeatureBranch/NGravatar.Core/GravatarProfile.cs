using System;
using System.Xml.Linq;
using System.Linq;

using NGravatar.Abstractions.Xml.Linq;

namespace NGravatar {

    /// <summary>
    /// Class to provide functionality for dealing with Gravatar profile information.
    /// </summary>
    public class GravatarProfile {

        private XDocument LoadXDocument(string email) {
            return XDocumentAbstraction.Load(GetXmlApiUrl(email));
        }

        internal XDocumentAbstraction XDocumentAbstraction {
            get {
                if (null == _XDocumentAbstraction) _XDocumentAbstraction = XDocumentAbstraction.Default;
                return _XDocumentAbstraction;
            }
            set {
                if (null == value) throw new ArgumentNullException("XDocumentAbstraction");
                _XDocumentAbstraction = value;
            }
        }
        private XDocumentAbstraction _XDocumentAbstraction;

        public Gravatar Gravatar {
            get {
                if (null == _Gravatar) _Gravatar = new Gravatar();
                return _Gravatar;
            }
            set {
                if (null == value) throw new ArgumentNullException("Gravatar");
                _Gravatar = value;
            }
        }
        private Gravatar _Gravatar;

        /// <summary>
        /// Gets the URL that links to the Gravatar profile of the specified <paramref name="email"/>.
        /// </summary>
        /// <param name="email">The email whose profile should be linked.</param>
        /// <returns>The URL of the profile for the specified <paramref name="email"/>.</returns>
        public string GetUrl(string email) {
            return "http://www.gravatar.com/" + Gravatar.GetEmailHash(email);
        }

        public string GetXmlApiUrl(string email) {
            return GetUrl(email) + ".xml";
        }

        public string GetJsonApiUrl(string email, string callback) {
            return string.IsNullOrEmpty(callback)
                ? string.Format("{0}.json", GetUrl(email))
                : string.Format("{0}.json?callback={1}", GetUrl(email), callback);
        }

        public string GetJsonApiUrl(string email) {
            return GetJsonApiUrl(email, null);
        }

        /// <summary>
        /// Parses Gravatar profile information for the specified <paramref name="email"/> into an object.
        /// </summary>
        /// <param name="email">The email whose profile information should be returned.</param>
        /// <returns>An object that contains information about the Gravatar profile for the specified <paramref name="email"/>.</returns>
        public GravatarProfileInformation LoadInformation(string email) {
            return new GravatarProfileInformation {
                Parser = new GravatarProfileParser {
                    Entry = LoadXDocument(email)
                        .Descendants("entry")
                        .First()
                }
            };
        }

        /// <summary>
        /// Creates a script tag that can be included in an HTML page to process a Gravatar profile on the client.
        /// </summary>
        /// <param name="email">The email whose profile should be processed.</param>
        /// <param name="callback">
        /// The JavaScript callback function which should be called after the profile information is loaded. The profile
        /// information will be passed as a paramter to this callback.
        /// </param>
        /// <returns>A rendered script tag that can be included in an HTML page.</returns>
        public string RenderScript(string email, string callback) {
            return string.Format(
                "<script type=\"text/javascript\" src=\"{0}\"></script>", 
                GetJsonApiUrl(email, callback)
            );
        }
    }
}

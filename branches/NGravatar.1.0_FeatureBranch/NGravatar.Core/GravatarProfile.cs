using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

using NGravatar.Utils;
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

        internal GravatarHasher Hasher {
            get {
                if (null == _Hasher) _Hasher = GravatarHasher.DefaultInstance;
                return _Hasher;
            }
            set {
                if (null == value) throw new ArgumentNullException("Hasher");
                _Hasher = value;
            }
        }
        private GravatarHasher _Hasher;

        internal HtmlBuilder HtmlBuilder {
            get {
                if (null == _HtmlBuilder) _HtmlBuilder = HtmlBuilder.DefaultInstance;
                return _HtmlBuilder;
            }
            set {
                if (null == value) throw new ArgumentNullException("HtmlBuilder");
                _HtmlBuilder = value;
            }
        }
        private HtmlBuilder _HtmlBuilder;

        /// <summary>
        /// Gets the URL that links to the Gravatar profile of the specified <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email whose profile should be linked.</param>
        /// <returns>The URL of the profile for the specified <paramref name="emailAddress"/>.</returns>
        public string GetUrl(string emailAddress) {
            return "http://www.gravatar.com/" + Hasher.Hash(emailAddress);
        }

        public string GetXmlApiUrl(string emailAddress) {
            return GetUrl(emailAddress) + ".xml";
        }

        public string GetJsonApiUrl(string emailAddress, string callback) {
            return string.IsNullOrEmpty(callback)
                ? string.Format("{0}.json", GetUrl(emailAddress))
                : string.Format("{0}.json?callback={1}", GetUrl(emailAddress), callback);
        }

        public string GetJsonApiUrl(string emailAddress) {
            return GetJsonApiUrl(emailAddress, null);
        }

        /// <summary>
        /// Parses Gravatar profile information for the specified <paramref name="emailAddress"/> into an object.
        /// </summary>
        /// <param name="emailAddress">The email whose profile information should be returned.</param>
        /// <returns>An object that contains information about the Gravatar profile for the specified <paramref name="emailAddress"/>.</returns>
        public GravatarProfileInformation LoadInformation(string emailAddress) {
            return new GravatarProfileInformation {
                Parser = new GravatarProfileParser {
                    Entry = LoadXDocument(emailAddress)
                        .Descendants("entry")
                        .First()
                }
            };
        }

        /// <summary>
        /// Creates a script tag that can be included in an HTML page to process a Gravatar profile on the client.
        /// </summary>
        /// <param name="emailAddress">The email whose profile should be processed.</param>
        /// <param name="callback">
        /// The JavaScript callback function which should be called after the profile information is loaded. The profile
        /// information will be passed as a paramter to this callback.
        /// </param>
        /// <returns>A rendered script tag that can be included in an HTML page.</returns>
        public string RenderScript(string emailAddress, string callback) {
            return HtmlBuilder.RenderScriptTag(new Dictionary<string, object> {
                { "type", "text/javascript" },
                { "src", GetJsonApiUrl(emailAddress, callback) }
            });
        }

        public string RenderLink(string emailAddress, string linkText, IDictionary<string, object> htmlAttributes) {
            htmlAttributes = htmlAttributes == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(htmlAttributes);
            htmlAttributes["href"] = GetUrl(emailAddress);
            return HtmlBuilder.RenderLinkTag(linkText, htmlAttributes);
        }
    }
}

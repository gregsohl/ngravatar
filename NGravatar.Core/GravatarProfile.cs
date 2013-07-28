using System;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;

using NGravatar.Utils;
using NGravatar.Abstractions.Xml.Linq;
using NGravatar.ProfileInformation;

namespace NGravatar {

    /// <summary>
    /// Class to provide functionality for dealing with Gravatar profile information.
    /// </summary>
    public class GravatarProfile {

        private XDocument LoadXDocument(string email, bool? useHttps) {
            return XDocumentAbstraction.Load(GetXmlApiUrl(email, useHttps));
        }

        internal XDocumentAbstraction XDocumentAbstraction {
            get {
                if (null == _XDocumentAbstraction) _XDocumentAbstraction = XDocumentAbstraction.DefaultInstance;
                return _XDocumentAbstraction;
            }
            set {
                if (null == value) throw new ArgumentNullException("XDocumentAbstraction");
                _XDocumentAbstraction = value;
            }
        }
        private XDocumentAbstraction _XDocumentAbstraction;

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
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>The URL of the profile for the specified <paramref name="emailAddress"/>.</returns>
        public string GetUrl(string emailAddress, bool? useHttps = null) {
            var g = Gravatar.DefaultInstance;
            return g.GetBaseUrl(useHttps) + "/" + g.GetHash(emailAddress);
        }

        /// <summary>
        /// Gets the API URL of the XML profile entry associated with the <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address of the account.</param>
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>
        /// The URL of the XML entry for the Gravatar account.
        /// </returns>
        public string GetXmlApiUrl(string emailAddress, bool? useHttps = null) {
            return GetUrl(emailAddress, useHttps) + ".xml";
        }

        /// <summary>
        /// Gets the API URL of the JSON profile entry associated with the <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address of the account.</param>
        /// <param name="callback">The Javascript callback function to be invoked when the requested data returns.</param>
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>
        /// The URL of the JSON entry for the Gravatar account.
        /// </returns>
        public string GetJsonApiUrl(string emailAddress, string callback = null, bool? useHttps = null) {
            var url = GetUrl(emailAddress, useHttps);
            return string.IsNullOrEmpty(callback)
                ? string.Format("{0}.json", url)
                : string.Format("{0}.json?callback={1}", url, callback);
        }

        /// <summary>
        /// Parses Gravatar profile information for the specified <paramref name="emailAddress"/> into an object.
        /// </summary>
        /// <param name="emailAddress">The email whose profile information should be returned.</param>
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>An object that contains information about the Gravatar profile for the specified <paramref name="emailAddress"/>.</returns>
        public GravatarProfileInformation LoadInformation(string emailAddress, bool? useHttps = null) {
            return new GravatarProfileInformation {
                Parser = new GravatarProfileParser {
                    Entry = XDocumentAbstraction
                        .Load(GetXmlApiUrl(emailAddress, useHttps))
                        .Descendants("entry")
                        .FirstOrDefault() ?? new XElement("entry")
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
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <returns>A rendered script tag that can be included in an HTML page.</returns>
        public string RenderScript(string emailAddress, string callback, bool? useHttps = null) {
            return HtmlBuilder.RenderScriptTag(new Dictionary<string, object> {
                { "type", "text/javascript" },
                { "src", GetJsonApiUrl(emailAddress, callback) }
            });
        }

        /// <summary>
        /// Creates an HTML link tag that points to the Gravatar profile of the <paramref name="emailAddress"/>.
        /// </summary>
        /// <param name="emailAddress">The email address of the account whose profile should be linked.</param>
        /// <param name="linkText">The inner text of the link tag.</param>
        /// <param name="useHttps">
        /// <c>true</c> to use the HTTPS Gravatar URL. Otherwise, <c>false</c>.
        /// This value can be <c>null</c> to use the <see cref="Gravatar.UseHttps"/> value
        /// of the <see cref="Gravatar.DefaultInstance"/>.
        /// </param>
        /// <param name="htmlAttributes">
        /// Additional HTML attributes to apply to the tag.
        /// </param>
        /// <returns>An HTML link tag that points to the Gravatar profile of the <paramref name="emailAddress"/>.</returns>
        public string RenderLink(string emailAddress, string linkText, bool? useHttps = null, IDictionary<string, object> htmlAttributes = null) {
            htmlAttributes = htmlAttributes == null
                ? new Dictionary<string, object>()
                : new Dictionary<string, object>(htmlAttributes);
            htmlAttributes["href"] = GetUrl(emailAddress, useHttps);
            return HtmlBuilder.RenderLinkTag(linkText, htmlAttributes);
        }
    }
}

using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections.Generic;

namespace NGravatar
{
    internal interface IGrofileHelper
    {
        XDocument LoadXml(string uri);
    }

    internal class GrofileHelper : IGrofileHelper
    {
        public XDocument LoadXml(string uri)
        {
            return XDocument.Load(uri);
        }
    }

    public class Grofile
    {
        #region Private Members
        private readonly IGrofileHelper _Helper = new GrofileHelper();
        #endregion

        #region Internal Members
        internal IGrofileHelper Helper { get { return _Helper; } }

        internal string GetXmlLink(string email)
        {
            return GetLink(email) + ".xml";
        }

        internal string GetJsonLink(string email)
        {
            return GetJsonLink(email, null);
        }

        internal string GetJsonLink(string email, string callback)
        {
            var link = GetLink(email) + ".json";
            if (!string.IsNullOrEmpty(callback))
                link += "?callback=" + callback;
            return link;
        }

        internal Grofile(IGrofileHelper helper)
        {
            if (null == helper) throw new ArgumentNullException("helper");
            _Helper = helper;
        }

        internal XDocument GetXDocument(string email)
        {
            return Helper.LoadXml(GetXmlLink(email));
        }
        #endregion

        #region Public Members
        public Grofile() { }

        public string GetLink(string email)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(email.Trim()))
                throw new ArgumentException("The email cannot be empty.", "email");

            return "http://www.gravatar.com/" + new Gremail(email).Hash();
        }

        public IGrofileInfo GetInfo(string email)
        {
            var xdoc = GetXDocument(email);
            var entry = xdoc.Descendants("entry").FirstOrDefault();
            if (entry == null) return null;
            return new GrofileInfoXml(entry);
        }

        public string RenderScript(string email, string callback)
        {
            var src = GetJsonLink(email, callback);
            var tag = "<script type=\"text/javascript\" src=\"" + src + "\"></script>";
            return tag;
        }
        #endregion
    }
}

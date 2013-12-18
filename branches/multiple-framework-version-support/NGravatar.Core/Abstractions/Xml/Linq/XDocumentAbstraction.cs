using System.Xml.Linq;

namespace NGravatar.Abstractions.Xml.Linq {

    internal class XDocumentAbstraction {

        protected XDocumentAbstraction() { }

        public static XDocumentAbstraction DefaultInstance { get { return _DefaultInstance; } }
        private static readonly XDocumentAbstraction _DefaultInstance = new XDocumentAbstraction();

        public virtual XDocument Load(string uri) {
            return XDocument.Load(uri);
        }
    }
}

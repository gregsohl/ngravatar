using System.Xml.Linq;

namespace NGravatar.Abstractions.Xml.Linq {

    internal class XDocumentAbstraction {

        protected XDocumentAbstraction() { }

        public static XDocumentAbstraction Default { get { return _Default; } }
        private static readonly XDocumentAbstraction _Default = new XDocumentAbstraction();

        public virtual XDocument Load(string uri) {
            return XDocument.Load(uri);
        }
    }
}

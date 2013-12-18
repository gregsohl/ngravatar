using System;
using System.Web;
using System.Linq;
using System.Collections.Generic;

namespace NGravatar.Utils {

    internal class HtmlBuilder {

        private string RenderAttributes(IDictionary<string, object> htmlAttributes) {
            if (null == htmlAttributes) throw new ArgumentNullException("htmlAttributes");

            var attributes = string.Empty;

            htmlAttributes.ToList().ForEach(pair => {
                var key = pair.Key;
                var value = pair.Value;
                attributes += (
                    value == null
                        ? string.Format("{0} ", HttpUtility.HtmlEncode(key))
                        : string.Format("{0}=\"{1}\" ", HttpUtility.HtmlEncode(key), HttpUtility.HtmlAttributeEncode(value.ToString()))
                );
            });

            return attributes.Trim();
        }

        public static HtmlBuilder DefaultInstance {
            get {
                if (null == _DefaultInstance) _DefaultInstance = new HtmlBuilder();
                return _DefaultInstance;
            }
            set {
                if (null == value) throw new ArgumentNullException("DefaultInstance");
                _DefaultInstance = value;
            }
        }
        private static HtmlBuilder _DefaultInstance;

        public virtual string RenderImageTag(string url = null, int? size = null, IDictionary<string, object> htmlAttributes = null) {

            if (htmlAttributes == null) {
                htmlAttributes = new Dictionary<string, object>();
            }

            if (url != null) {
                htmlAttributes["src"] = url;
            }

            if (size != null) {
                htmlAttributes["width"] = size;
                htmlAttributes["height"] = size;
            }

            return string.Format("<img {0} />", RenderAttributes(htmlAttributes));
        }

        public virtual string RenderLinkTag(string linkText, IDictionary<string, object> htmlAttributes) {
            return string.Format("<a {0}>{1}</a>", RenderAttributes(htmlAttributes), HttpUtility.HtmlEncode(linkText));
        }

        public virtual string RenderScriptTag(IDictionary<string, object> htmlAttributes) {
            return string.Format("<script {0}></script>", RenderAttributes(htmlAttributes));
        }
    }
}

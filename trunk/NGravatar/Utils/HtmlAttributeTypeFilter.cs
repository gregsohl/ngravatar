using System;
using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;

namespace NGravatar.Utils {

    internal class HtmlAttributeTypeFilter {

        public IDictionary<string, object> FilterToDictionary(object htmlAttributes) {
            if (null == htmlAttributes) return null;

            var collection = htmlAttributes as IEnumerable<KeyValuePair<string, object>>;
            return (collection ?? new RouteValueDictionary(htmlAttributes))
                .ToDictionary(
                    pair => pair.Key, 
                    pair => pair.Value
                );
        }
    }
}

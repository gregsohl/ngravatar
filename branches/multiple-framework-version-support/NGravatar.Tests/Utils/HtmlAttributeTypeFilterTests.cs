using System;
using System.Web.Routing;
using System.Linq;
using System.Collections.Generic;

using NUnit.Framework;

namespace NGravatar.Utils.Tests {

    [TestFixture]
    public class HtmlAttributeTypeFilterTests {

        [Test]
        public void FilterToDictionary_ReturnsNullForNullArgument() {
            Assert.IsNull(new HtmlAttributeTypeFilter().FilterToDictionary(null));
        }

        [Test]
        public void FilterToDictionary_ReturnsDictionaryForDictionaryArgument() {
            var dict1 = new Dictionary<string, object> {
                { "val1", 1 },
                { "val2", "two" },
                { "val3", null }
            };
            var dict2 = new HtmlAttributeTypeFilter().FilterToDictionary(dict1);
            CollectionAssert.AreEqual(dict1, dict2);
        }

        [Test]
        public void FilterToDictionary_ReturnsDictionaryForObject() {
            var obj = new {
                value1 = 100,
                value2 = "2",
                value3 = default(object)
            };
            var expected = new Dictionary<string, object> {
                { "value1", 100 },
                { "value2", "2" },
                { "value3", null }
            };
            var actual = new HtmlAttributeTypeFilter().FilterToDictionary(obj);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}

using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace NGravatar.Utils.Tests {

    [TestFixture]
    public class HtmlBuilderTests {

        [Test]
        public void DefaultInstance_IsInitiallyInstance() {
            var def = HtmlBuilder.DefaultInstance;
            Assert.IsNotNull(def);
            Assert.AreEqual(typeof(HtmlBuilder), def.GetType());
        }

        [Test]
        public void DefaultInstance_CanBeSet() {
            var def = new HtmlBuilder();
            Assert.AreNotSame(def, HtmlBuilder.DefaultInstance);
            HtmlBuilder.DefaultInstance = def;
            Assert.AreSame(def, HtmlBuilder.DefaultInstance);
        }

        [Test]
        public void DefaultInstance_ThrowsExceptionIfNull() {
            try {
                HtmlBuilder.DefaultInstance = null;
            }
            catch (Exception ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                return;
            }
            Assert.Fail("No exception thrown.");
        }

        [Test]
        public void RenderImageTag_EscapesAttributes() {
            var actual = new HtmlBuilder().RenderImageTag(htmlAttributes: new Dictionary<string, object> {
                { "class", "ok" },
                { "data-script", "\"><script type='text/javascript' src='badscript.js'></script><img \"" },
                { "id", 53 }
            });
            var expected = "<img class=\"ok\" data-script=\"&quot;>&lt;script type=&#39;text/javascript&#39; src=&#39;badscript.js&#39;>&lt;/script>&lt;img &quot;\" id=\"53\" />";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RenderScriptTag_EscapesAttributes() {
            var actual = new HtmlBuilder().RenderScriptTag(new Dictionary<string, object> {
                { "id", 9 },
                { "src", "http://dom.com/script.js?a=1&b=2&c=3" }
            });
            var expected = "<script id=\"9\" src=\"http://dom.com/script.js?a=1&amp;b=2&amp;c=3\"></script>";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RenderLinkTag_EscapesAttributesAndLinkText() {
            var actual = new HtmlBuilder().RenderLinkTag("<a href=\"link.com\">Click me!</a>", new Dictionary<string, object> {
                { "class", "big" },
                { "title", "<strong>hello world</strong>" }
            });
            var expected = "<a class=\"big\" title=\"&lt;strong>hello world&lt;/strong>\">&lt;a href=&quot;link.com&quot;&gt;Click me!&lt;/a&gt;</a>";
            Assert.AreEqual(expected, actual);
        }
    }
}

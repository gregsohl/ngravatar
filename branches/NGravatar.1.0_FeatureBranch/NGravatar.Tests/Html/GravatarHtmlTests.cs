using System;
using System.Web.Mvc;
using System.Collections.Generic;

using NUnit.Framework;

namespace NGravatar.Html.Tests {

    [TestFixture]
    public class GravatarHtmlTests {

        [Test]
        public void Gravatar_ReturnsRenderedHtml() {
            var email = "ngravatar@kendoll.net";
            var size = 110;
            var defaultImage = "pathtodefault.img";
            var maxRating = GravatarRating.R;
            var attributes = new Dictionary<string, object>
            {
                { "name1", "val1" },
                { "name2", "val2" }
            };

            var gravatarHtml = GravatarHtml.Gravatar(null, email, size, defaultImage, maxRating, attributes);

            var gravatar = new Gravatar();
            gravatar.Size = size;
            gravatar.Rating = maxRating;
            gravatar.Default = defaultImage;

            Assert.AreEqual(MvcHtmlString.Create(gravatar.Render(email, htmlAttributes: attributes)).ToString(), gravatarHtml.ToString());
        }

        [Test]
        public void Gravatar_RendersHtmlWithAttributes() {
            var email = "ngravatar@kendoll.net";
            var size = 110;
            var defaultImage = "pathtodefault.img";
            var maxRating = GravatarRating.R;
            var attributes = new Dictionary<string, object>
            {
                { "name1", "val1" },
                { "name2", "val2" }
            };

            var gravatarHtm1 = GravatarHtml.Gravatar(null, email, size, defaultImage, maxRating, attributes);
            var gravatarHtm2 = GravatarHtml.Gravatar(null, email, size, defaultImage, maxRating, new { name1 = "val1", name2 = "val2" });

            Assert.AreEqual(gravatarHtm1.ToString(), gravatarHtm2.ToString());
        }

        [Test]
        public void Gravatar_RendersHtmlWithDefaultAttributes() {
            var email = "ngravatar@kendoll.net";
            var size = 110;

            var gravatarHtm1 = GravatarHtml.Gravatar(null, email, size, null, null, null);
            var gravatarHtm2 = GravatarHtml.Gravatar(null, email, size);

            Assert.AreEqual(gravatarHtm1.ToString(), gravatarHtm2.ToString());
        }

        [Test]
        public void GravatarProfileLink_RendersLink() {
            var email = "some@email.com";
            var linkText = "linktext";
            var href = new GravatarProfile().GetUrl(email);
            var attr = new Dictionary<string, object> { { "rel", "grofile" } };
            var expected = "<a rel=\"grofile\" href=\"" + href + "\">linktext</a>";
            var actual = GravatarHtml.GravatarProfileLink(null, email, linkText, attr);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void GravatarProfileLink_RendersLinkWithAttributes() {
            var email = "some@email.com";
            var href = new GravatarProfile().GetUrl(email);
            var attr = new { rel = "grofile", @class = "myclass" };
            var expected = "<a rel=\"grofile\" class=\"myclass\" href=\"" + href + "\">linktext</a>";
            var actual = GravatarHtml.GravatarProfileLink(null, email, "linktext", attr);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void GravatarProfileLink_RendersLinkWithNoAttributes() {
            var email = "some@email.com";
            var href = new GravatarProfile().GetUrl(email);
            var expected = "<a href=\"" + href + "\">linktext</a>";
            var actual = GravatarHtml.GravatarProfileLink(null, email, "linktext");
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void GravatarProfileScript_RendersScriptTag() {
            var email = "some@email.com";
            var callback = "mycallback";
            var src = new GravatarProfile().GetUrl(email) + ".json?callback=" + callback;
            var expected = "<script type=\"text/javascript\" src=\"" + src + "\"></script>";
            var actual = GravatarHtml.GravatarProfileScript(null, email, callback);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }
    }
}

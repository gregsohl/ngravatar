using System;
using System.Collections.Generic;

using NUnit.Framework;

using NGravatar.Utils;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarTests {

        [Test]
        public void HtmlBuilder_IsInitiallyDefaultInstance() {
            Assert.AreSame(HtmlBuilder.DefaultInstance, new Gravatar().HtmlBuilder);
        }

        [Test]
        public void DefaultInstance_IsInitiallyDefault() {
            var expected = new Gravatar();
            var actual = Gravatar.DefaultInstance;
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Default, actual.Default);
            Assert.AreEqual(expected.Rating, actual.Rating);
            Assert.AreEqual(expected.RenderedSize, actual.RenderedSize);
            Assert.AreEqual(expected.Size, actual.Size);
        }

        [Test]
        public void DefaultInstance_CanBeSet() {
            var expected = new Gravatar();
            Assert.AreNotSame(expected, Gravatar.DefaultInstance);
            Gravatar.DefaultInstance = expected;
            Assert.AreSame(expected, Gravatar.DefaultInstance);
        }

        [Test]
        public void DefaultInstance_ThrowsExceptionIfNull() {
            try {
                Gravatar.DefaultInstance = null;
            }
            catch (Exception ex) {
                Assert.IsInstanceOf<ArgumentNullException>(ex);
                return;
            }
            Assert.Fail("No exception thrown.");
        }

        [Test]
        public void Rating_IsInitiallyNull() {
            Assert.IsNull(new Gravatar().Rating);
        }

        [Test]
        public void RenderedSize_IsInitially80() {
            Assert.AreEqual(80, new Gravatar().RenderedSize);
        }

        [Test]
        public void Size_IsInitiallyNull() {
            Assert.IsNull(new Gravatar().Size);
        }

        [Test]
        public void Default_IsInitiallyNull() {
            Assert.IsNull(new Gravatar().Default);
        }

        [Test]
        public void GetEmailHash_HashesEmailAddress() {
            var actual = new Gravatar().GetHash("ngravatar@kendoll.net");
            var expected = "bccc2b381d103797427c161951be5fa5";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_GetsUrlWithDefaultValues() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net");
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesRating() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", rating: GravatarRating.R);
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?r=r";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesSize() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", size: 43);
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?s=43";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesDefault() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", @default: "http://my.default.image.jpg");
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?d=http%3a%2f%2fmy.default.image.jpg";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesInstanceProperties() {
            var gravatar = new Gravatar {
                Default = "https://some.avatar.png/image",
                Rating = GravatarRating.PG,
                Size = 94
            };
            var actual = gravatar.GetUrl("ngravatar@kendoll.net");
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?s=94&r=pg&d=https%3a%2f%2fsome.avatar.png%2fimage";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesParameterSizeBeforeInstanceSize() {
            var gravatar = new Gravatar { Size = 88 };
            var actual = gravatar.GetUrl("ngravatar@kendoll.net", size: 87);
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?s=87";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesParameterRatingBeforeInstanceRating() {
            var gravatar = new Gravatar { Rating = GravatarRating.G };
            var actual = gravatar.GetUrl("ngravatar@kendoll.net", rating: GravatarRating.X);
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?r=x";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_IncludesForceDefault() {
            var gravatar = new Gravatar { Default = "my_default.jpg" };
            var actual = gravatar.GetUrl("ngravatar@kendoll.net", forceDefault: true);
            var expected = "http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?d=my_default.jpg&f=y";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Render_CreatesHtmlElement() {
            var actual = new Gravatar().Render("ngravatar@kendoll.net");
            var expected = "<img src=\"http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?s=80\" width=\"80\" height=\"80\" />";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Render_IncludesHtmlAttributes() {
            var actual = new Gravatar().Render("ngravatar@kendoll.net", htmlAttributes: new Dictionary<string, object> { { "class", "class-name" }, { "id", "idValue" } });
            var expected = "<img class=\"class-name\" id=\"idValue\" src=\"http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?s=80\" width=\"80\" height=\"80\" />";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Render_EscapesHtml() {
            var attrs = new Dictionary<string, object> {
                { "title", "\"><script src=\"oops.js\"></script>\"" }
            };
            var actual = new Gravatar().Render("ngravatar@kendoll.net", htmlAttributes: attrs);
            var expected = "<img title=\"&quot;>&lt;script src=&quot;oops.js&quot;>&lt;/script>&quot;\" src=\"http://www.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5?s=80\" width=\"80\" height=\"80\" />";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesHttps() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", useHttps: true);
            var expected = "https://secure.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5";
            Assert.AreEqual(expected, actual);
        }
    }
}

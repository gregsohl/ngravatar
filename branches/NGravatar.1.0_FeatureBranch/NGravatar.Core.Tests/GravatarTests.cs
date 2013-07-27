using System;
using System.Collections.Generic;

using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarTests {

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
            var actual = new Gravatar().GetEmailHash("ngravatar@kendoll.net");
            var expected = "bccc2b381d103797427c161951be5fa5";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_GetsUrlWithDefaultValues() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net");
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesRating() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", rating: GravatarRating.R);
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&rating=R";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesSize() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", size: 43);
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&size=43";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesDefault() {
            var actual = new Gravatar().GetUrl("ngravatar@kendoll.net", @default: "http://my.default.image.jpg");
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&default=http%3a%2f%2fmy.default.image.jpg";
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
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&rating=PG&size=94&default=https%3a%2f%2fsome.avatar.png%2fimage";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesParameterSizeBeforeInstanceSize() {
            var gravatar = new Gravatar { Size = 88 };
            var actual = gravatar.GetUrl("ngravatar@kendoll.net", size: 87);
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&size=87";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesParameterRatingBeforeInstanceRating() {
            var gravatar = new Gravatar { Rating = GravatarRating.G };
            var actual = gravatar.GetUrl("ngravatar@kendoll.net", rating: GravatarRating.X);
            var expected = "http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&rating=X";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Render_CreatesHtmlElement() {
            var actual = new Gravatar().Render("ngravatar@kendoll.net");
            var expected = "<img src=\"http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&size=80\" width=\"80\" height=\"80\" />";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Render_IncludesHtmlAttributes() {
            var actual = new Gravatar().Render("ngravatar@kendoll.net", htmlAttributes: new Dictionary<string, string> { { "class", "class-name" }, { "id", "idValue" } });
            var expected = "<img src=\"http://www.gravatar.com/avatar.php?gravatar_id=bccc2b381d103797427c161951be5fa5&size=80\" width=\"80\" height=\"80\" class=\"class-name\" id=\"idValue\" />";
            Assert.AreEqual(expected, actual);
        }
    }
}

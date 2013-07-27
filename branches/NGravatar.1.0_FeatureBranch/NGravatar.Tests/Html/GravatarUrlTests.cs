using System;

using NUnit.Framework;

namespace NGravatar.Html.Tests {

    [TestFixture]
    public class GravatarUrlTests {

        [Test]
        public void GravatarTest() {
            var size = 274;
            var email = "ngravatar@kendoll.net";
            var defaultImage = "pathtodefault.img";
            var maxRating = GravatarRating.X;
            var gravatarSrc = GravatarUrl.Gravatar(null, email, size, defaultImage, maxRating);

            var gravatar = new Gravatar();
            gravatar.Size = size;
            gravatar.Default = defaultImage;
            gravatar.Rating = maxRating;

            Assert.AreEqual(gravatar.GetUrl(email), gravatarSrc);
        }

        [Test]
        public void GravatarTest2() {
            var size = 274;
            var email = "ngravatar@kendoll.net";
            var gravatar1 = GravatarUrl.Gravatar(null, email, size, null, null);
            var gravatar2 = GravatarUrl.Gravatar(null, email, size);

            Assert.AreEqual(gravatar1, gravatar2);
        }

        [Test]
        public void GrofileTest() {
            var email = "some@email.com";
            var expected = new GravatarProfile().GetUrl(email);
            var actual = GravatarUrl.Grofile(null, email);
            Assert.AreEqual(expected, actual);
        }
    }
}

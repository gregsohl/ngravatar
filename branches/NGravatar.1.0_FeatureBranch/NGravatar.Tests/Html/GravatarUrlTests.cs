using System;

using NUnit.Framework;

namespace NGravatar.Html.Tests {

    [TestFixture]
    public class GravatarUrlTests {

        [Test]
        public void Gravatar_ReturnsUrl() {
            var size = 274;
            var email = "ngravatar@kendoll.net";
            var defaultImage = "pathtodefault.img";
            var maxRating = GravatarRating.X;
            var gravatarSrc = GravatarUrl.Gravatar(null, email, size, maxRating, defaultImage);

            var gravatar = new Gravatar();
            gravatar.Size = size;
            gravatar.Default = defaultImage;
            gravatar.Rating = maxRating;

            Assert.AreEqual(gravatar.GetUrl(email), gravatarSrc);
        }

        [Test]
        public void Gravatar_ReturnsUrlWithDefaultAttributes() {
            var size = 274;
            var email = "ngravatar@kendoll.net";
            var gravatar1 = GravatarUrl.Gravatar(null, email, size, null, null);
            var gravatar2 = GravatarUrl.Gravatar(null, email, size);

            Assert.AreEqual(gravatar1, gravatar2);
        }

        [Test]
        public void GravatarProfile_ReturnsUrl() {
            var email = "some@email.com";
            var expected = new GravatarProfile().GetUrl(email);
            var actual = GravatarUrl.GravatarProfile(null, email);
            Assert.AreEqual(expected, actual);
        }
    }
}

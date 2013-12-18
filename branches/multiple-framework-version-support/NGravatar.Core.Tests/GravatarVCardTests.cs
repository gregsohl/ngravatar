using System;

using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarVCardTests {

        [Test]
        public void GetUrl_ReturnsVCardUrl() {
            var email = "my@mail.me";
            var gravatar = Gravatar.DefaultInstance = new Gravatar();
            var expected = gravatar.GetBaseUrl() + "/" + gravatar.GetHash(email) + ".vcf";
            var actual = new GravatarVCard().GetUrl(email);
            Assert.AreEqual(expected, actual);
        }
    }
}

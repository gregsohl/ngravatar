using System;

using Moq;
using NUnit.Framework;

using NGravatar.Utils;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarQrCodeTests {

        [Test]
        public void HtmlBuilder_IsInitiallyInstance() {
            var hb = new GravatarQrCode().HtmlBuilder;
            Assert.IsNotNull(hb);
            Assert.AreEqual(typeof(HtmlBuilder), hb.GetType());
        }

        [Test]
        public void GetUrl_GetsQrCodeUrl() {
            var email = "some@email.net";
            var gravatar = Gravatar.DefaultInstance = new Gravatar();
            var expected = gravatar.GetBaseUrl() + "/" + gravatar.GetHash(email) + ".qr";
            var actual = new GravatarQrCode().GetUrl(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesHttps() {
            var email = "email@address.com";
            var gravatar = Gravatar.DefaultInstance = new Gravatar();
            var expected = gravatar.GetBaseUrl(useHttps: true) + "/" + gravatar.GetHash(email) + ".qr";
            var actual = new GravatarQrCode().GetUrl(email, useHttps: true);
            Assert.AreEqual(expected, actual);
            Gravatar.DefaultInstance.UseHttps = true;
            actual = new GravatarQrCode().GetUrl(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetUrl_UsesSize() {
            var size = 97;
            var email = "example@domain.us";
            var gravatar = Gravatar.DefaultInstance = new Gravatar();
            var expected = gravatar.GetBaseUrl() + "/" + gravatar.GetHash(email) + ".qr?s=" + size;
            var actual = new GravatarQrCode().GetUrl(email, size: size);
            Assert.AreEqual(expected, actual);
            Gravatar.DefaultInstance.Size = size;
            actual = new GravatarQrCode().GetUrl(email);
            Assert.AreEqual(expected, actual);
        }
    }
}

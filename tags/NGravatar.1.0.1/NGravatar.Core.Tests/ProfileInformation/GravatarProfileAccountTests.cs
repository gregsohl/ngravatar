using System;

using NUnit.Framework;

namespace NGravatar.ProfileInformation.Tests {

    [TestFixture]
    public class GravatarProfileAccountTests {

        [Test]
        public void Constructor_SetsProperties() {
            var a = new GravatarProfileAccount("domain", "username", "display", "url", "shortname", true);
            Assert.AreEqual("domain", a.Domain);
            Assert.AreEqual("username", a.Username);
            Assert.AreEqual("display", a.Display);
            Assert.AreEqual("url", a.Url);
            Assert.AreEqual("shortname", a.Shortname);
            Assert.IsTrue(a.Verified);
        }

        [Test]
        public void ToString_ReturnsDisplay() {
            var a = new GravatarProfileAccount(null, null, "expected", null, null, false);
            Assert.AreEqual("expected", a.ToString());
        }
    }
}

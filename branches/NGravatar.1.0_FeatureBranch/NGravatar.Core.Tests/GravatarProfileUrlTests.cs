using System;

using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarProfileUrlTests {

        [Test]
        public void Constructor_SetsProperties() {
            var u = new GravatarProfileUrl("title", "value");
            Assert.AreEqual("title", u.Title);
            Assert.AreEqual("value", u.Value);
            Assert.IsNull(u.Type);
            Assert.IsFalse(u.Primary);
        }
    }
}

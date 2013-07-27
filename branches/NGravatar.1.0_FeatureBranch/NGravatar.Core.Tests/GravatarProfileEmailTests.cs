using System;

using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarProfileEmailTests {

        [Test]
        public void Constructor_SetsProperties() {
            var e = new GravatarProfileEmail("value", true);
            Assert.AreEqual("value", e.Value);
            Assert.IsTrue(e.Primary);
        }

        [Test]
        public void Constructor_SetsPrimaryToFalse() {
            var e = new GravatarProfileEmail("v");
            Assert.AreEqual("v", e.Value);
            Assert.IsFalse(e.Primary);
        }
    }
}

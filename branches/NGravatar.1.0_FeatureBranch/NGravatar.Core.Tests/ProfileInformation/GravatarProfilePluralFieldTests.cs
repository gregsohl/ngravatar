using System;

using NUnit.Framework;

namespace NGravatar.ProfileInformation.Tests {

    [TestFixture]
    public class GravatarProfilePluralFieldTests {

        [Test]
        public void Constructor_SetsProperties() {
            var f = new GravatarProfilePluralField("value", "type", false);
            Assert.AreEqual("value", f.Value);
            Assert.AreEqual("type", f.Type);
            Assert.IsFalse(f.Primary);
        }

        [Test]
        public void Constructor_SetsTypeToNull() {
            var f = new GravatarProfilePluralField("val", true);
            Assert.AreEqual("val", f.Value);
            Assert.IsNull(f.Type);
            Assert.IsTrue(f.Primary);
        }

        [Test]
        public void Constructor_SetsPrimaryToFalse() {
            var f = new GravatarProfilePluralField("v", "t");
            Assert.AreEqual("v", f.Value);
            Assert.AreEqual("t", f.Type);
            Assert.IsFalse(f.Primary);
        }

        [Test]
        public void Constructor_SetsTypeToNullAndPrimaryToFalse() {
            var f = new GravatarProfilePluralField("value");
            Assert.AreEqual("value", f.Value);
            Assert.IsNull(f.Type);
            Assert.IsFalse(f.Primary);
        }

        [Test]
        public void Constructor_SetsDefaults() {
            var f = new GravatarProfilePluralField();
            Assert.IsNull(f.Value);
            Assert.IsNull(f.Type);
            Assert.IsFalse(f.Primary);
        }

        [Test]
        public void ToString_ReturnsValue() {
            var f = new GravatarProfilePluralField("expected");
            Assert.AreEqual("expected", f.ToString());
        }
    }
}

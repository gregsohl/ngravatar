using System;

using NUnit.Framework;

namespace NGravatar.ProfileInformation.Tests {

    [TestFixture]
    public class GravatarProfileNameTests {

        [Test]
        public void Constructor_SetsProperties() {
            var n = new GravatarProfileName("Jon Doe", "Doe", "Jon", "Emmanuel", "NA", "N/A");
            Assert.AreEqual("Jon Doe", n.Formatted);
            Assert.AreEqual("Doe", n.FamilyName);
            Assert.AreEqual("Jon", n.GivenName);
            Assert.AreEqual("Emmanuel", n.MiddleName);
            Assert.AreEqual("NA", n.HonorificPrefix);
            Assert.AreEqual("N/A", n.HonorificSuffix);
        }

        [Test]
        public void ToString_ReturnsFormatted() {
            var n = new GravatarProfileName("Jane Doe", null, null, null, null, null);
            Assert.AreEqual("Jane Doe", n.ToString());
        }
    }
}

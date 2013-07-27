using System;

using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravagarProfilePhotoTests {

        [Test]
        public void Constructor_SetsProperties() {
            var p = new GravatarProfilePhoto("value", "type");
            Assert.AreEqual("value", p.Value);
            Assert.AreEqual("type", p.Type);
        }

        [Test]
        public void Constructor_SetsTypeToNull() {
            var p = new GravatarProfilePhoto("v");
            Assert.AreEqual("v", p.Value);
            Assert.IsNull(p.Type);
        }
    }
}

using System;

using NUnit.Framework;

using NGravatar.Abstractions.Xml.Linq;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarProfileTests {

        [Test]
        public void XDocumentAbstraction_IsInitiallyInstance() {
            Assert.AreEqual(typeof(XDocumentAbstraction), new GravatarProfile().XDocumentAbstraction.GetType());
        }

        [Test]
        public void Gravatar_IsInitiallyNewInstance() {
            var actual = new GravatarProfile().Gravatar;
            var expected = new Gravatar();
            Assert.AreEqual(expected.Default, actual.Default);
            Assert.AreEqual(expected.Rating, actual.Rating);
            Assert.AreEqual(expected.RenderedSize, actual.RenderedSize);
            Assert.AreEqual(expected.Size, actual.Size);
        }
    }
}

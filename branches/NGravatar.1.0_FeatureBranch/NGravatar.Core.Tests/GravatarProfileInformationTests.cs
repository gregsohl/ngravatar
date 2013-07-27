using System;

using Moq;
using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarProfileInformationTests {

        [Test]
        public void Parser_IsInitiallyNewInstance() {
            Assert.IsNotNull(new GravatarProfileInformation().Parser);
        }

        [Test]
        public void Exists_UsesEntryExistsOfParser() {

            foreach (var exists in new[] { true, false }) {
                var parser = new Mock<GravatarProfileParser>(MockBehavior.Strict);

                var count = 0;
                parser.Setup(p => p.EntryExists()).Returns(delegate {
                    count++;
                    return exists;
                });

                var info = new GravatarProfileInformation { Parser = parser.Object };
                Assert.AreEqual(exists, info.Exists);
                Assert.AreEqual(exists, info.Exists);
                Assert.AreEqual(1, count);
            }
        }

        [Test]
        public void Name_IsNullForEmptyProfile() {
            Assert.IsNull(new GravatarProfileInformation().Name);
        }

        [Test]
        public void Name_ComesFromParsedName() {
            var parser = new Mock<GravatarProfileParser>(MockBehavior.Strict);
            var name = new GravatarProfileName("", "", "", "", "", "");
            var count = 0;
            parser.Setup(p => p.ParseName()).Returns(delegate {
                count++;
                return name;
            });
            var info = new GravatarProfileInformation { Parser = parser.Object };
            Assert.AreSame(name, info.Name);
            Assert.AreSame(name, info.Name);
            Assert.AreEqual(1, count);
        }
    }
}

using System;

using NUnit.Framework;

namespace NGravatar.Core.Tests {

    [TestFixture]
    public class GravatarRatingTests {

        [Test]
        public void G_IsLessThanPG() {
            Assert.IsTrue(GravatarRating.G < GravatarRating.PG);
        }

        [Test]
        public void PG_IsLessThanR() {
            Assert.IsTrue(GravatarRating.PG < GravatarRating.R);
        }

        [Test]
        public void R_IsLessThanX() {
            Assert.IsTrue(GravatarRating.R < GravatarRating.X);
        }
    }
}
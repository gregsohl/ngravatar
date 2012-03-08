using NUnit.Framework;
using System;

namespace NGravatar.Html.Tests
{
    [TestFixture]
    public class UrlHelperTests
    {
        [Test]
        public void GravatarTest()
        {
            var size = 274;
            var email = "ngravatar@kendoll.net";
            var defaultImage = "pathtodefault.img";
            var maxRating = Rating.X;
            var gravatarSrc = UrlHelperExtensions.Gravatar(null, email, size, defaultImage, maxRating);
            
            var gravatar = new Gravatar();
            gravatar.Size = size;
            gravatar.DefaultImage = defaultImage;
            gravatar.MaxRating = maxRating;
            
            Assert.AreEqual(gravatar.GetImageSource(email), gravatarSrc);
        }
        
        [Test]
        public void GravatarTest2()
        {
            var size = 274;
            var email = "ngravatar@kendoll.net";
            var gravatar1 = UrlHelperExtensions.Gravatar(null, email, size, null, null);
            var gravatar2 = UrlHelperExtensions.Gravatar(null, email, size);
            
            Assert.AreEqual(gravatar1, gravatar2);
        }

        [Test]
        public void GrofileTest()
        {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email);
            var actual = UrlHelperExtensions.Grofile(null, email);
            Assert.AreEqual(expected, actual);
        }
    }
}

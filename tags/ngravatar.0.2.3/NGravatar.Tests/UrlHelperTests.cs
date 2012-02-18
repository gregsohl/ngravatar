using NUnit.Framework;
using System;
using NGravatar.Html;

namespace NGravatar.Tests
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
    }
}

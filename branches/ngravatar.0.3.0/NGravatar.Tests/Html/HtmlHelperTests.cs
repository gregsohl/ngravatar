using NUnit.Framework;
using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace NGravatar.Html.Tests
{
    [TestFixture]
    public class HtmlHelperTests
    {
        [Test]
        public void GravatarTest()
        {
            var email = "ngravatar@kendoll.net";
            var size = 110;
            var defaultImage = "pathtodefault.img";
            var maxRating = Rating.R;
            var attributes = new Dictionary<string, string>
            {
                { "name1", "val1" },
                { "name2", "val2" }
            };
            
            var gravatarHtml = HtmlHelperExtenions.Gravatar(null, email, size, defaultImage, maxRating, attributes);
            
            var gravatar = new Gravatar();
            gravatar.Size = size;
            gravatar.MaxRating = maxRating;
            gravatar.DefaultImage = defaultImage;
            
            Assert.AreEqual(MvcHtmlString.Create(gravatar.Render(email, attributes)).ToHtmlString(), gravatarHtml.ToHtmlString());               
        }
        
        [Test]
        public void GravatarTest2()
        {
            var email = "ngravatar@kendoll.net";
            var size = 110;
            var defaultImage = "pathtodefault.img";
            var maxRating = Rating.R;
            var attributes = new Dictionary<string, string>
            {
                { "name1", "val1" },
                { "name2", "val2" }
            };
            
            var gravatarHtm1 = HtmlHelperExtenions.Gravatar(null, email, size, defaultImage, maxRating, attributes);
            var gravatarHtm2 = HtmlHelperExtenions.Gravatar(null, email, size, defaultImage, maxRating, new { name1 = "val1", name2 = "val2" });
            
            Assert.AreEqual(gravatarHtm1.ToHtmlString(), gravatarHtm2.ToHtmlString());
        }
        
        [Test]
        public void GravatarTest3()
        {
            var email = "ngravatar@kendoll.net";
            var size = 110;
            
            var gravatarHtm1 = HtmlHelperExtenions.Gravatar(null, email, size, null, null, null);   
            var gravatarHtm2 = HtmlHelperExtenions.Gravatar(null, email, size);
            
            Assert.AreEqual(gravatarHtm1.ToHtmlString(), gravatarHtm2.ToHtmlString());
        }
    }
}

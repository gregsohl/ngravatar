using NUnit.Framework;
using System;
using System.Xml.Linq;
using System.Collections.Generic;

namespace NGravatar.Tests
{
    [TestFixture]
    public class GravatarTests
    {
        [Test]
        public void DefaultSizeTest()
        {
            Assert.AreEqual(80, new Gravatar().Size);
        }
        
        [Test]
        public void DefaultMaxRatingTest()
        {
            Assert.AreEqual(Rating.PG, new Gravatar().MaxRating);
        }
        
        [Test]
        public void DefaultDefaultImageTest()
        {
            Assert.IsNull(new Gravatar().DefaultImage);   
        }
        
        [Test]
        public void GetImageSourceTest()
        {
            var email = "ngravatar@kendoll.net";
            var gravatar = new Gravatar();
            var src = gravatar.GetImageSource(email);
            var uri = new Uri(src);
            var query = uri.Query;
            
            var encoder = new System.Text.UTF8Encoding();
            var md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            var hashedBytes = md5.ComputeHash(encoder.GetBytes(email.ToLower()));
            var sb = new System.Text.StringBuilder(hashedBytes.Length * 2);

            for (var i = 0; i < hashedBytes.Length; i++)
                sb.Append(hashedBytes[i].ToString("X2"));            
            
            Assert.AreEqual("www.gravatar.com", uri.Host);
            Assert.AreEqual("/avatar.php", uri.AbsolutePath);
            Assert.IsTrue(query.Contains("gravatar_id=" + sb.ToString().ToLower()));
            Assert.IsTrue(query.Contains("size=" + gravatar.Size.ToString()));
            Assert.IsTrue(query.Contains("rating=" + gravatar.MaxRating.ToString()));
            Assert.IsFalse(query.Contains("default="));
        }
        
        [Test]
        public void GetImageSourceTest2()
        {
            var email = "ngravatar@kendoll.net";
            var gravatar = new Gravatar();
            gravatar.DefaultImage = "path/to/default.img";
            
            var src = gravatar.GetImageSource(email);
            var uri = new Uri(src);
            var query = uri.Query;
            
            Assert.IsTrue(query.Contains("default=" + System.Web.HttpUtility.UrlEncode(gravatar.DefaultImage)));                                                                                      
        }
        
        [Test]
        public void RenderTest()
        {
            var email = "ngravatar@kendoll.net";
            var gravatar = new Gravatar();
            var img = gravatar.Render(email);
            img = img.Replace("&", "&amp;");
            
            var imgEl = XElement.Parse(img);
            
            Assert.AreEqual("img", imgEl.Name.LocalName);
            Assert.AreEqual(gravatar.GetImageSource(email), imgEl.Attribute("src").Value.Replace("&amp;", "&"));
            Assert.AreEqual(gravatar.Size.ToString(), imgEl.Attribute("width").Value);
            Assert.AreEqual(gravatar.Size.ToString(), imgEl.Attribute("height").Value);
            Assert.AreEqual(string.Empty, imgEl.Value);
        }
        
        [Test]
        public void RenderTest2()
        {
            var email = "ngravatar@kendoll.net";
            var gravatar = new Gravatar();
            var img = gravatar.Render(email, new Dictionary<string, string>
            {
                { "name1", "val1" },
                { "name2", "val2" },
                { "src", "wrongsource" },
                { "width", "wrongwidth" },
                { "height", "wrongheight" }
            });
            img = img.Replace("&", "&amp;");
            
            var imgEl = XElement.Parse(img);  
            
            Assert.AreEqual("val1", imgEl.Attribute("name1").Value);
            Assert.AreEqual("val2", imgEl.Attribute("name2").Value);
            Assert.AreEqual(gravatar.GetImageSource(email), imgEl.Attribute("src").Value.Replace("&amp;", "&"));
            Assert.AreEqual(gravatar.Size.ToString(), imgEl.Attribute("width").Value);
            Assert.AreEqual(gravatar.Size.ToString(), imgEl.Attribute("height").Value);
        }
    }
}

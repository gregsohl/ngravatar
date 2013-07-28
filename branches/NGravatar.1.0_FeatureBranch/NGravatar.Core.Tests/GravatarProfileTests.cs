using System;
using System.Xml.Linq;
using System.Linq;

using Moq;
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
        public void GetUrl_ReturnsUrl() {
            Assert.AreEqual("http://www.gravatar.com/bccc2b381d103797427c161951be5fa5", new GravatarProfile().GetUrl("ngravatar@kendoll.net"));
        }

        [Test]
        public void GetXmlApiUrl_ReturnsXmlLocation() {
            var p = new GravatarProfile();
            Assert.AreEqual(p.GetUrl("some@email.com") + ".xml", p.GetXmlApiUrl("some@email.com"));
        }

        [Test]
        public void GetJsonApiUrl_ReturnsJsonLocation() {
            var p = new GravatarProfile();
            Assert.AreEqual(p.GetUrl("email@some.net") + ".json", p.GetJsonApiUrl("email@some.net"));
        }

        [Test]
        public void GetJsonApiUrl_AddsCallback() {
            var p = new GravatarProfile();
            Assert.AreEqual("http://www.gravatar.com/bccc2b381d103797427c161951be5fa5.json?callback=my_callback", p.GetJsonApiUrl("ngravatar@kendoll.net", "my_callback"));
        }

        [Test]
        public void RenderScript_CreatesScriptTag() {
            var p = new GravatarProfile();
            var s = p.GetJsonApiUrl("some@email.me", "process");
            var expected = "<script type=\"text/javascript\" src=\"" + s + "\"></script>";
            var actual = p.RenderScript("some@email.me", "process");
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void LoadInformation_LoadsXml() {
            var p = new GravatarProfile();
            var uri = p.GetXmlApiUrl("my@email.com");
            var xdoc = XDocument.Parse("<entry><someelement /></entry>");
            var abstraction = Mock.Of<XDocumentAbstraction>(d => d.Load(uri) == xdoc);

            p.XDocumentAbstraction = abstraction;

            var information = p.LoadInformation("my@email.com");
            Assert.AreSame(xdoc.Descendants("entry").First(), information.Parser.Entry);
        }

        [Test]
        public void RenderLink_EscapesLinkText() {
            var p = new GravatarProfile();
            var url = p.GetUrl("some@domain.com");
            var expected = "<a href=\"" + url + "\">&lt;script type=&quot;text/javascript&quot; src=&#39;bad.js&#39;&gt;&lt;/script&gt;</a>";
            var actual = p.RenderLink("some@domain.com", "<script type=\"text/javascript\" src='bad.js'></script>");
            Assert.AreEqual(expected, actual);
        }
    }
}

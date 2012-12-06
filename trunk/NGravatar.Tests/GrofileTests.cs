using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NGravatar.Tests {

    [TestFixture]
    public class GrofileTests {

        class MyGrofileHelper : IGrofileHelper {
            public string Xml { get; set; }

            public XDocument LoadXml(string uri) {
                return XDocument.Parse(Xml);
            }
        }

        [Test]
        public void ConstructorTest() {
            var grofile = new Grofile();
            Assert.AreEqual(typeof(GrofileHelper), grofile.Helper.GetType());
        }

        [Test]
        public void GetLinkTest() {
            var email = "some@email.com";
            var expected = "http://www.gravatar.com/" + new Gremail(email).Hash();
            var actual = new Grofile().GetLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetXmlLinkTest() {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email) + ".xml";
            var actual = new Grofile().GetXmlLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetJsonLinkTest() {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email) + ".json";
            var actual = new Grofile().GetJsonLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetJsonLinkTest2() {
            var email = "some@email.com";
            var callback = "mycallback";
            var expected = new Grofile().GetLink(email) + ".json?callback=" + callback;
            var actual = new Grofile().GetJsonLink(email, callback);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetXDocumentTest() {
            var email = "some@email.com";
            var xml = "<expectedXml email=\"" + email + "\" />";
            var helper = new MyGrofileHelper { Xml = xml };
            var grofile = new Grofile(helper);
            var expected = XDocument.Parse(xml);
            var actual = grofile.GetXDocument(email);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void GetInfoTest() {
            var id = "1234";
            var hash = "5678";
            var xml = "<response><entry><id>" + id + "</id><hash>" + hash + "</hash></entry></response>";
            var helper = new MyGrofileHelper { Xml = xml };
            var grofile = new Grofile(helper);
            var xel = XElement.Parse(xml).Element("entry");
            var expected = new GrofileInfoXml(xel);
            var actual = grofile.GetInfo("some@email");
            Assert.AreEqual(id, expected.Id);
            Assert.AreEqual(hash, expected.Hash);
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Hash, actual.Hash);
        }

        [Test]
        public void GetInfoTest2() {
            var xml = "<response></response>";
            var helper = new MyGrofileHelper { Xml = xml };
            var grofile = new Grofile(helper);
            var actual = grofile.GetInfo("some@email.com");
            Assert.IsNull(actual);
        }

        [Test]
        public void GetInfoExceptionTest() {
            foreach (var xml in new[] { null, string.Empty, "<invalid<xml> />", "junk" }) {
                try { new Grofile(new MyGrofileHelper { Xml = xml }).GetInfo("some@email.com"); }
                catch { continue; }
                Assert.Fail("No exception thrown.");
            }
        }

        [Test]
        public void RenderScriptTest() {
            var email = "some@email.com";
            var callback = "mycallback";
            var src = new Grofile().GetJsonLink(email) + "?callback=" + callback;
            var expected = "<script type=\"text/javascript\" src=\"" + src + "\"></script>";
            var actual = new Grofile().RenderScript(email, callback);
            Assert.AreEqual(expected, actual);
        }
    }
}

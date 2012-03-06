using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace NGravatar.Tests
{
    [TestFixture]
    public class GrofileTests
    {
        class MyGrofileHelper : IGrofileHelper
        {
            public string Xml { get; set; }

            public XDocument LoadXml(string uri)
            {
                return XDocument.Parse(Xml);
            }

            public string LoadString(string uri)
            {
                return uri;
            }
        }

        [Test]
        public void ConstructorTest()
        {
            var grofile = new Grofile();
            Assert.AreEqual(typeof(GrofileHelper), grofile.Helper.GetType());
        }

        [Test]
        public void GetLinkTest()
        {
            var email = "some@email.com";
            var expected = "http://www.gravatar.com/" + new Gremail(email).Hash();
            var actual = new Grofile().GetLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetXmlLinkTest()
        {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email) + ".xml";
            var actual = new Grofile().GetXmlLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetJsLinkTest()
        {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email) + ".js";
            var actual = new Grofile().GetJsLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetJsonLinkTest()
        {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email) + ".json";
            var actual = new Grofile().GetJsonLink(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetXmlTest()
        {
            var email = "some@email.com";
            var expected = new Grofile().GetLink(email) + ".xml";
            var actual = new Grofile(new MyGrofileHelper()).GetXml(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetJsTest()
        {
            var email = "some@email.com";
            var grofile = new Grofile(new MyGrofileHelper());
            var expected = grofile.GetLink(email) + ".js";
            var actual = grofile.GetJs(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetJsonTest()
        {
            var email = "some@email.com";
            var grofile = new Grofile(new MyGrofileHelper());
            var expected = grofile.GetLink(email) + ".json";
            var actual = grofile.GetJson(email);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void GetXDocumentTest()
        {
            var email = "some@email.com";
            var xml = "<expectedXml email=\"" + email + "\" />";
            var helper = new MyGrofileHelper { Xml = xml };
            var grofile = new Grofile(helper);
            var expected = XDocument.Parse(xml);
            var actual = grofile.GetXDocument(email);
            Assert.AreEqual(expected.ToString(), actual.ToString());
        }

        [Test]
        public void GetInfoTest()
        {
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
    }
}

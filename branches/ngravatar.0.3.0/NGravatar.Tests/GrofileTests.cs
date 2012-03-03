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
    }
}

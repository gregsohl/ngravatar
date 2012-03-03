using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace NGravatar.Tests
{
    [TestFixture]
    public class XElementExtensionsTests
    {
        [Test]
        public void ElementValueOrDefaultTest()
        {
            var xml = "<xel><el1>el1text</el1></xel>";
            var xel = XElement.Parse(xml);
            Assert.AreEqual("el1text", xel.ElementValueOrDefault("el1", null));
            Assert.AreEqual(null, xel.ElementValueOrDefault("el2", null));
        }
    }

    [TestFixture]
    public class GrNameTests
    {
        [Test]
        public void ToStringTest()
        {
            var formatted = "Some Name";
            var name = new GrName(formatted, null, null, null, null, null);
            Assert.AreEqual(formatted, name.ToString());
        }
    }

    [TestFixture]
    public class GrofileInfoTests
    {
        [Test]
        public void IdTest()
        {
            var expected = Entry.Element("id").Value;
            var actual = InfoXml.Id;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void HashTest()
        {
            var expected = Entry.Element("hash").Value;
            var actual = InfoXml.Hash;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void RequestHashTest()
        {
            var expected = Entry.Element("requestHash").Value;
            var actual = InfoXml.RequestHash;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ProfileUrlTest()
        {
            var expected = Entry.Element("profileUrl").Value;
            var actual = InfoXml.ProfileUrl;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void PreferredUsernameTest()
        {
            var expected = Entry.Element("preferredUsername").Value;
            var actual = InfoXml.PreferredUsername;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ThumbnailUrlTest()
        {
            var expected = Entry.Element("thumbnailUrl").Value;
            var actual = InfoXml.ThumbnailUrl;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void DisplayNameTest()
        {
            var expected = Entry.Element("displayName").Value;
            var actual = InfoXml.DisplayName;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void AboutMeTest()
        {
            var expected = Entry.Element("aboutMe").Value;
            var actual = InfoXml.AboutMe;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void CurrentLocationTest()
        {
            var expected = Entry.Element("currentLocation").Value;
            var actual = InfoXml.CurrentLocation;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void NameTest()
        {
            var formatted = Entry.ElementValueOrDefault("formatted", null);
            var familyName = Entry.ElementValueOrDefault("familyName", null);
            var givenName = Entry.ElementValueOrDefault("givenName", null);
            var middleName = Entry.ElementValueOrDefault("middleName", null);
            var honorificPrefix = Entry.ElementValueOrDefault("honorificPrefix", null);
            var honorificSuffix = Entry.ElementValueOrDefault("honorificSuffix", null);

            var name = new GrName(formatted, familyName, givenName, middleName, honorificPrefix, honorificSuffix);
            var actual = InfoXml.Name;

            Assert.AreEqual(name.FamilyName, actual.FamilyName);
            Assert.AreEqual(name.Formatted, actual.Formatted);
            Assert.AreEqual(name.GivenName, actual.GivenName);
            Assert.AreEqual(name.HonorificPrefix, actual.HonorificPrefix);
            Assert.AreEqual(name.HonorificSuffix, actual.HonorificSuffix);
            Assert.AreEqual(name.MiddleName, actual.MiddleName);
        }

        [Test]
        public void UrlsTest()
        {
            var list = new List<GrUrl>();
            foreach (var element in Entry.Elements("urls"))
                list.Add(new GrUrl(element.Element("title").Value, element.Element("value").Value));
            var urls = InfoXml.Urls;
            for (var i = 0; i < list.Count; i++)
            {
                var expected = list[i];
                var actual = urls.ElementAt(i);
                Assert.AreEqual(expected.Title, actual.Title);
                Assert.AreEqual(expected.Value, actual.Value);
            }
        }

        private GrofileInfoXml InfoXml
        {
            get { return new GrofileInfoXml(Entry); }
        }

        private XElement Entry
        {
            get
            {
                return XElement.Parse(@"
<entry>
	<id>32922842</id>
	<hash>bccc2b381d103797427c161951be5fa5</hash>
	<requestHash>ngravatar</requestHash>
	<profileUrl>http://gravatar.com/ngravatar</profileUrl>
	<preferredUsername>ngravatar</preferredUsername>

	<thumbnailUrl>http://2.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5</thumbnailUrl>
	<photos>
		<value>http://2.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5</value>
		<type>thumbnail</type>
	</photos>
	<photos>
		<value>http://0.gravatar.com/userimage/32922842/8766872a3b38265fdec89a08843001c3</value>

	</photos>
	<photos>
		<value>http://2.gravatar.com/userimage/32922842/c3c5baa63482fd02d34d1aa20e67b4e2</value>
	</photos>
	<name>
		<givenName>N</givenName>
		<familyName>Gravatar</familyName>

		<formatted>NGravatar</formatted>
	</name>
	<displayName>ngravatar</displayName>
	<aboutMe>NGravatar is a .NET library for getting profile and avatar information from gravatar.com.</aboutMe>
	<currentLocation>Chicago</currentLocation>
	<emails>
		<value>ngravatar@kendoll.net</value>

		<primary>true</primary>
	</emails>
	<accounts>
		<domain>twitter.com</domain>
		<username>kendollDotNet</username>
		<display>@kendollDotNet</display>
		<url>http://twitter.com/kendollDotNet</url>

		<verified>true</verified>
		<shortname>twitter</shortname>
	</accounts>
	<urls>
		<title>kendoll.net</title>
		<value>http://kendoll.net</value>
	</urls>

	<urls>
		<title>Google Code</title>
		<value>http://code.google.com/p/ngravatar/</value>
	</urls>
	<urls>
		<title>NuGet</title>
		<value>http://nuget.org/packages/NGravatar</value>

	</urls>
</entry>
                ");
            }
        }
    }
}

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
            var name = new GrofileName(formatted, null, null, null, null, null);
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

            var name = new GrofileName(formatted, familyName, givenName, middleName, honorificPrefix, honorificSuffix);
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
            var list = new List<GrofileUrl>();
            foreach (var element in Entry.Elements("urls"))
                list.Add(new GrofileUrl(element.Element("title").Value, element.Element("value").Value));
            var urls = InfoXml.Urls;
            for (var i = 0; i < list.Count; i++)
            {
                var expected = list[i];
                var actual = urls.ElementAt(i);
                Assert.AreEqual(expected.Title, actual.Title);
                Assert.AreEqual(expected.Value, actual.Value);
            }
        }

        [Test]
        public void EmailsTest()
        {
            var list = new List<GrofileEmail>();
            foreach (var element in Entry.Elements("emails"))
            {
                var value = element.Element("value").Value;
                var primary = bool.Parse(element.ElementValueOrDefault("primary", "false"));
                list.Add(new GrofileEmail(value, primary));
            }
            var emails = InfoXml.Emails;
            for (var i = 0; i < list.Count; i++)
            {
                var expected = list[i];
                var actual = emails.ElementAt(i);
                Assert.AreEqual(expected.Value, actual.Value);
                Assert.AreEqual(expected.Primary, actual.Primary);
            }
        }

        [Test]
        public void PhotosTest()
        {
            var list = new List<GrofilePhoto>();
            foreach (var element in Entry.Elements("photos"))
            {
                var value = element.Element("value").Value;
                var type = element.ElementValueOrDefault("type", null);
                list.Add(new GrofilePhoto(value, type));
            }
            var photos = InfoXml.Photos;
            for (var i = 0; i < list.Count; i++)
            {
                var expected = list[i];
                var actual = photos.ElementAt(i);
                Assert.AreEqual(expected.Value, actual.Value);
                Assert.AreEqual(expected.Type, actual.Type);
            }
        }

        [Test]
        public void AccountsTest()
        {
            var list = new List<GrofileAccount>();
            foreach (var element in Entry.Elements("accounts"))
            {
                var domain = element.Element("domain").Value;
                var username = element.Element("username").Value;
                var display = element.Element("display").Value;
                var url = element.Element("url").Value;
                var verified = element.Element("verified").Value;
                var shortname = element.Element("shortname").Value;
                list.Add(new GrofileAccount(domain, username, display, url, shortname, bool.Parse(verified)));
            }
            var accounts = InfoXml.Accounts;
            for (var i = 0; i < list.Count; i++)
            {
                var expected = list[i];
                var actual = accounts.ElementAt(i);
                Assert.AreEqual(expected.Domain, actual.Domain);
                Assert.AreEqual(expected.Username, actual.Username);
                Assert.AreEqual(expected.Display, actual.Display);
                Assert.AreEqual(expected.Url, actual.Url);
                Assert.AreEqual(expected.Shortname, actual.Shortname);
                Assert.AreEqual(expected.Verified, actual.Verified);
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

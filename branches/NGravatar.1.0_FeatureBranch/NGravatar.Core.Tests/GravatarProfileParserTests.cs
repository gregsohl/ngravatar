using System;
using System.Xml.Linq;
using System.Linq;

using NUnit.Framework;

namespace NGravatar.Tests {

    [TestFixture]
    public class GravatarProfileParserTests {

        [Test]
        public void ParseId_ReturnsId() {
            Assert.AreEqual("32922842", GetParser().ParseId());
        }

        [Test]
        public void ParseHash_ReturnsHash() {
            Assert.AreEqual("bccc2b381d103797427c161951be5fa5", GetParser().ParseHash());
        }

        [Test]
        public void ParseRequestHash_ReturnsRequestHash() {
            Assert.AreEqual("bccc2b381d103797427c161951be5fa5", GetParser().ParseRequestHash());
        }

        [Test]
        public void ParseProfileUrl_ReturnsProfileUrl() {
            Assert.AreEqual("http://gravatar.com/ngravatar", GetParser().ParseProfileUrl());
        }

        [Test]
        public void ParsePreferredUsername_ReturnsPreferredUsername() {
            Assert.AreEqual("ngravatar", GetParser().ParsePreferredUsername());
        }

        [Test]
        public void ParseThumbnailUrl_ReturnsThumbnailUrl() {
            Assert.AreEqual("http://2.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5", GetParser().ParseThumbnailUrl());
        }

        [Test]
        public void ParseDisplayName_ReturnsDisplayName() {
            Assert.AreEqual("ngravatar", GetParser().ParseDisplayName());
        }

        [Test]
        public void ParseAboutMe_ReturnsAboutMe() {
            Assert.AreEqual("NGravatar is a .NET library for getting profile and avatar information from gravatar.com.", GetParser().ParseAboutMe());
        }

        [Test]
        public void ParseCurrentLocation_ReturnsCurrentLocation() {
            Assert.AreEqual("Chicago", GetParser().ParseCurrentLocation());
        }

        [Test]
        public void ParseName_ReturnsName() {
            var n = GetParser().ParseName();
            Assert.IsNotNull(n);
            Assert.AreEqual("N", n.GivenName);
            Assert.AreEqual("Gravatar", n.FamilyName);
            Assert.AreEqual("NGravatar", n.Formatted);
        }

        [Test]
        public void ParsePhotos_ReturnsPhotos() {
            var p = GetParser().ParsePhotos();
            Assert.IsNotNull(p);
            Assert.AreEqual(3, p.Count());
            var p0 = p.ElementAt(0);
            var p1 = p.ElementAt(1);
            var p2 = p.ElementAt(2);
            Assert.AreEqual("http://2.gravatar.com/avatar/bccc2b381d103797427c161951be5fa5", p0.Value);
            Assert.AreEqual("thumbnail", p0.Type);
            Assert.AreEqual("http://0.gravatar.com/userimage/32922842/8766872a3b38265fdec89a08843001c3", p1.Value);
            Assert.IsNull(p1.Type);
            Assert.AreEqual("http://2.gravatar.com/userimage/32922842/c3c5baa63482fd02d34d1aa20e67b4e2", p2.Value);
            Assert.IsNull(p2.Type);
        }

        [Test]
        public void ParseEmails_ReturnsEmails() {
            var e = GetParser().ParseEmails();
            Assert.IsNotNull(e);
            Assert.AreEqual(1, e.Count());
            var e0 = e.ElementAt(0);
            Assert.AreEqual("ngravatar@kendoll.net", e0.Value);
            Assert.IsTrue(e0.Primary);                                  
        }

        [Test]
        public void ParseUrls_ReturnsUrls() {
            var u = GetParser().ParseUrls();
            Assert.AreEqual(3, u.Count());
            var u0 = u.ElementAt(0);
            var u1 = u.ElementAt(1);
            var u2 = u.ElementAt(2);
            Assert.AreEqual("kendoll.net", u0.Title);
            Assert.AreEqual("http://kendoll.net", u0.Value);
            Assert.AreEqual("Google Code", u1.Title);
            Assert.AreEqual("http://code.google.com/p/ngravatar/", u1.Value);
            Assert.AreEqual("NuGet", u2.Title);
            Assert.AreEqual("http://nuget.org/packages/NGravatar", u2.Value);
        }

        [Test]
        public void ParseAccounts_ReturnsAccounts() {
            var a = GetParser().ParseAccounts();
            Assert.IsNotNull(a);
            Assert.AreEqual(1, a.Count());
            var a0 = a.ElementAt(0);
            Assert.AreEqual("twitter.com", a0.Domain);
            Assert.AreEqual("kendollDotNet", a0.Username);
            Assert.AreEqual("@kendollDotNet", a0.Display);
            Assert.AreEqual("http://twitter.com/kendollDotNet", a0.Url);
            Assert.IsTrue(a0.Verified);
            Assert.AreEqual("twitter", a0.Shortname);
        }

        private GravatarProfileParser GetParser() {
            return new GravatarProfileParser { Entry = GetEntry() };
        }

        private XElement GetEntry() {
            return XDocument.Parse(Response).Root.Descendants("entry").First();
        }

        private string Response = 
            @"<?xml version='1.0' encoding='utf-8'?>
            <response>
                <entry>
                    <id>32922842</id>
                    <hash>bccc2b381d103797427c161951be5fa5</hash>
                    <requestHash>bccc2b381d103797427c161951be5fa5</requestHash>
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
            </response>
        ";
    }
}

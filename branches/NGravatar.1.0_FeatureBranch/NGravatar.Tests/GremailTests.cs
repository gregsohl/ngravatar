using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGravatar.Tests {

    [TestFixture]
    public class GremailTests {

        [Test]
        public void HashTest() {
            var email = "MyEmailAddress@example.com ";
            Assert.AreEqual("0bc83cb571cd1c50ba6f3e8a78ef1346", new Gremail(email).Hash());
        }

        [Test]
        public void TrimTest() {
            var email1 = "some@email.com";
            var email2 = "\tsome@email.com ";
            var email3 = "\n some@email.com \t \r";
            var hash1 = new Gremail(email1).Hash();
            var hash2 = new Gremail(email2).Hash();
            var hash3 = new Gremail(email3).Hash();

            Assert.AreEqual(hash1, hash2);
            Assert.AreEqual(hash2, hash3);
        }

        [Test]
        public void ToLowerTest() {
            var email1 = "some@email.com";
            var email2 = "Some@Email.Com";
            var email3 = "sOMe@EMaiL.cOM";
            var hash1 = new Gremail(email1).Hash();
            var hash2 = new Gremail(email2).Hash();
            var hash3 = new Gremail(email3).Hash();

            Assert.AreEqual(hash1, hash2);
            Assert.AreEqual(hash2, hash3);
        }
    }
}

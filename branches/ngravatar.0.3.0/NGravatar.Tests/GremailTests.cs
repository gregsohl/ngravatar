using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NGravatar.Tests
{
    [TestFixture]
    public class GremailTests
    {
        [Test]
        public void HashTest()
        {
            var email = "MyEmailAddress@example.com ";
            Assert.AreEqual("0bc83cb571cd1c50ba6f3e8a78ef1346", new Gremail(email).Hash());
        }
    }
}

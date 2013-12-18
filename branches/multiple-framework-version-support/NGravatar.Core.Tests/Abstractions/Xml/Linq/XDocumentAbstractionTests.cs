using System;
using System.IO;
using System.Xml.Linq;

using NUnit.Framework;

namespace NGravatar.Abstractions.Xml.Linq.Tests {

    [TestFixture]
    public class XDocumentAbstractionTests {

        [Test]
        public void DefaultInstance_IsInstance() {
            var def = XDocumentAbstraction.DefaultInstance;
            Assert.IsNotNull(def);
            Assert.AreEqual(typeof(XDocumentAbstraction), def.GetType());
        }

        [Test]
        public void Load_LoadsXDocument() {
            var tempFile = Path.GetTempFileName();
            try {
                File.WriteAllText(tempFile, 
                    @"<?xml version='1.0' encoding='utf-8'?>
                        <rootelement>
                            <element1>
                                <value1>1000</value1>
                            </element1>
                        </rootelement>" 
                );
                var actual = XDocumentAbstraction.DefaultInstance.Load(tempFile).ToString();
                var expected = XDocument.Load(tempFile).ToString();
                Assert.AreEqual(expected, actual);
            }
            finally {
                File.Delete(tempFile);
            }
        }
    }
}

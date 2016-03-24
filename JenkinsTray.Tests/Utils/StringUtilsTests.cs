using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using JenkinsTray.Utils;

namespace JenkinsTray.Tests.Utils
{
    [TestFixture]
    public class StringUtilsTests
    {
        [Test]
        public void TestSimpleName()
        {
            Assert.AreEqual("User", StringUtils.ExtractUserName("User"));
        }

        [Test]
        public void TestCompoundName()
        {
            Assert.AreEqual("User Name", StringUtils.ExtractUserName("User Name"));
        }

        [Test]
        public void TestNameWithDash()
        {
            Assert.AreEqual("User-Name", StringUtils.ExtractUserName("User-Name"));
        }

        [Test]
        public void TestTrimming()
        {
            Assert.AreEqual("User", StringUtils.ExtractUserName("User    "));
        }

        [Test]
        public void TestAngleBrackets()
        {
            Assert.AreEqual("User Name", StringUtils.ExtractUserName("User Name <user@example.com>"));
            Assert.AreEqual("<user@example.com>", StringUtils.ExtractUserName("<user@example.com>"));
        }

        [Test]
        public void TestBrackets()
        {
            Assert.AreEqual("User Name", StringUtils.ExtractUserName("User Name (user@example.com)"));
            Assert.AreEqual("(user@example.com)", StringUtils.ExtractUserName("(user@example.com)"));
        }
    }
}

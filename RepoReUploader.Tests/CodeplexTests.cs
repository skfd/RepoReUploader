using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoReUploader.Tests
{
    using System.IO;

    using RepoReUploader.Downloader;

    [TestFixture]
    public class CodeplexTests
    {
        [Test]
        public void SimpleUrlTranslatesToSvnUrl()
        {
            var res = CodeplexUrlBuilder.BuildSvnUrl("https://SvnThief.codeplex.com/");

            Assert.AreEqual(res, new Uri("https://SvnThief.svn.codeplex.com/svn"));
        }

        [Test]
        public void SimpleUrlTranslatesToGitUrl()
        {
            var res = CodeplexUrlBuilder.BuildGitUrl("https://gitThief.codeplex.com/");

            Assert.AreEqual(res, new Uri("https://git01.codeplex.com/gitthief"));
        }

        [Test]
        public void SvnCloned()
        {
            var svc = new CodeplexService("SvnThief");

            svc.Clone("https://SvnThief.codeplex.com/");
        }

        [Test]
        public void GitCloned()
        {
            var svc = new CodeplexService("gitThief");

            svc.Clone("https://gitThief.codeplex.com/");
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
            Directory.Delete(".", recursive: true);
        }
    }
}

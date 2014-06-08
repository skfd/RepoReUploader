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
            var svc = new CodeplexService();

            svc.Clone("https://SvnThief.codeplex.com/", "SvnThief");
        }

        [Test]  
        public void GitCloned()
        {
            var svc = new CodeplexService();

            svc.Clone("https://gitThief.codeplex.com/", "gitThief");
        }

        [Test]
        public void FullTest()
        {
            var rru = new ReUploader(
                @"C:\reuploades\",
                @"C:\reuploades\config.json");

            rru.ReUpload();
        }


        [TestFixtureTearDown]
        public void TearDown()
        {
            //Directory.Delete(".", recursive: true);
        }
    }
}

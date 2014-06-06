using NUnit.Framework;
using RepoReUploader.Uploader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoReUploader.Tests
{
    [TestFixture]
    public class UploaderTests
    {
        [Test]
        public void OrganisationsReposRetrievedLive()
        {
            var d = new GithubService();

            var repos = d.GetPublicRepos("octokit");

            Assert.IsNotEmpty(repos);
        }
    }
}

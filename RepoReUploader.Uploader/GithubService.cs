using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Octokit;

namespace RepoReUploader.Uploader
{
    public class GithubService
    {
        public GithubService()
        {
            this.Github = new GitHubClient(new ProductHeaderValue("RepoReUploader"));
        }
        public IReadOnlyList<string> GetPublicRepos(string organisationName)
        {
            var org = Github.Repository.GetAllForOrg(organisationName).Result;

            var repos = org.Select(x => x.Name).ToList();

            return repos;
        }

        public GitHubClient Github { get; set; }
    }
}

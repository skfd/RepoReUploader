using Newtonsoft.Json;
using RepoReUploader.Downloader;
using RepoReUploader.Uploader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoReUploader
{
    public class ReUploader
    {
        private GithubService gh;
        private CodeplexService cp;
        private IReadOnlyList<string> repos;
        private Config config;
        private IEnumerable<string> existingRepos;
        private IEnumerable<string> newRepos;
        private string cacheFolder;
        public ReUploader(string mainCacheFolderPath, string configPath)
        {
            this.cacheFolder = mainCacheFolderPath;

            this.ReadConfig(configPath);

            this.existingRepos = this.FindExistingRepos(mainCacheFolderPath);

            this.newRepos = this.FindNewRepos(existingRepos);


            this.cp = new CodeplexService();

            this.gh = new GithubService();

            this.repos = gh.GetPublicRepos("ms-crm");
        }

        private IEnumerable<string> FindNewRepos(IEnumerable<string> existing)
        {
            if (this.config == null)
            {
                return new List<string>();
            }

            var newRepos =
                this.config.Repos.Select(x =>
                    x.ToLowerInvariant())
                .Except(
                    existing.Select(x => x.ToLowerInvariant()));

            return newRepos;
        }

        private IEnumerable<string> FindExistingRepos(string mainCacheFolderPath)
        {
            var folders = Directory.GetDirectories(mainCacheFolderPath).Select(x => new DirectoryInfo(x).Name);

            return folders;
        }

        private void ReadConfig(string configPath)
        {
            File.Exists(configPath);

            var str = File.ReadAllText(configPath);

            this.config = JsonConvert.DeserializeObject<Config>(str);
        }
        public void ReUpload()
        {
            this.cp.CloneAll(existingRepos, this.cacheFolder);

            this.cp.CloneAll(newRepos, this.cacheFolder);

            var repos = this.gh.GetPublicRepos("ms-crm");

            
            // + read config
            // + find all local repos
            // + what repos are new?
            // + clone new repos
            //    get license and description
            // update old repos

            // csv2git updated repos
            // push new and updated repos
        }
    }
}

namespace RepoReUploader.Downloader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SharpGit;

    using SharpSvn;
    using System.IO;

    public class CodeplexService
    {
        public CodeplexService()
        {
        }

        public void Clone(string projectName, string repoFolder)
        {
            var svnUrl = CodeplexUrlBuilder.BuildSvnUrl(projectName);

            var svnResult = this.SvnClone(svnUrl, repoFolder);

            if (svnResult)
            {
                return;
            }

            var gitUrl = CodeplexUrlBuilder.BuildGitUrl(projectName);

            if (Directory.Exists(repoFolder))
            {
                this.GitPull(repoFolder);
            }
            else
            {
                var gitResult = this.GitClone(gitUrl, repoFolder);
            }
        }

        public void CloneAll(IEnumerable<string> repos, string mainFolder)
        {
            foreach (var repo in repos)
            {
                var folder = Path.Combine(mainFolder, repo);

                this.Clone(repo, folder);

            }
        }

        private object GitClone(Uri uri, string repoFolder)
        {
             using (var client = new GitClient())
             {
                 Console.WriteLine("checking out {0}...", uri);

                 try
                 {
                     var res = client.Clone(uri, repoFolder);

                     if (res)
                     {
                         Console.WriteLine("success!");
                     }
                     else
                     {
                         Console.WriteLine("fail :(");
                     }

                     return res;
                 }
                 catch (SvnRepositoryIOException ex)
                 {
                     Console.WriteLine("looks like it's not git url.");

                     return false;
                 }
             }
        }

        private bool GitPull(string repoFolder)
        {
            using(var client = new GitClient())
            {
                var res = client.Pull(repoFolder);

                if (res)
                {
                    Console.WriteLine("Pull succeeded");
                }
                else
                {
                    Console.WriteLine("Pull failed!");
                }

                return res;
            }
        }

        private bool SvnClone(Uri uri, string repoFolder)
        {
            using (var client = new SvnClient())
            {
                Console.WriteLine("checking out {0}...", uri);

                try
                {
                    var res = client.CheckOut(uri, repoFolder);

                    if (res)
                    {
                        Console.WriteLine("success!");
                    }
                    else
                    {
                        Console.WriteLine("fail :(");
                    }

                    return res;
                }
                catch (SvnRepositoryIOException ex)
                {
                    Console.WriteLine("looks like it's not svn url.");

                    return false;
                }

            }
        }
    }
}

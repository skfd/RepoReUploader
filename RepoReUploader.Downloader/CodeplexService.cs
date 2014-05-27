namespace RepoReUploader.Downloader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using SharpGit;

    using SharpSvn;

    public class CodeplexService
    {
        private string cacheFolder;

        public CodeplexService(string cacheFolder)
        {
            this.cacheFolder = cacheFolder;
        }

        public void Clone(string url)
        {
            var svnUrl = CodeplexUrlBuilder.BuildSvnUrl(url);

            var svnResult = this.SvnClone(svnUrl);

            if (svnResult)
            {
                return;
            }

            var gitUrl = CodeplexUrlBuilder.BuildGitUrl(url);

            var gitResult = this.GitClone(gitUrl);


        }

        private object GitClone(Uri uri)
        {
             using (var client = new GitClient())
             {
                 Console.WriteLine("checking out {0}...", uri);

                 try
                 {
                     var res = client.Clone(uri, cacheFolder);

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

        private bool SvnClone(Uri uri)
        {
            using (var client = new SvnClient())
            {
                Console.WriteLine("checking out {0}...", uri);

                try
                {
                    var res = client.CheckOut(uri, cacheFolder);

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

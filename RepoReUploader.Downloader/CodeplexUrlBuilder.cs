namespace RepoReUploader.Downloader
{
    using System;

    public static class CodeplexUrlBuilder
    {
        public static Uri BuildGitUrl(string url)
        {
            var uri = new Uri(url);

            var host = uri.GetComponents(UriComponents.Host, UriFormat.Unescaped);

            var projectName = host.Split('.')[0];

            var result = string.Format("https://git01.codeplex.com/{0}", projectName);

            return new Uri(result);
        }

        public static Uri BuildSvnUrl(string url)
        {
            var uri = new Uri(url);

            var host = uri.GetComponents(UriComponents.Host, UriFormat.Unescaped);

            var projectName = host.Split('.')[0];

            var result = string.Format("https://{0}.svn.codeplex.com/svn", projectName);

            return new Uri(result);
        }
    }
}
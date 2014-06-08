namespace RepoReUploader.Downloader
{
    using System;

    public static class CodeplexUrlBuilder
    {
        public static Uri BuildGitUrl(Uri uri)
        {
            var host = uri.GetComponents(UriComponents.Host, UriFormat.Unescaped);

            var projectName = host.Split('.')[0];

            var result = string.Format("https://git01.codeplex.com/{0}", projectName);

            return new Uri(result);
        }

        public static Uri BuildGitUrl(string projectName)
        {
            var result = string.Format("https://git01.codeplex.com/{0}", projectName);

            return new Uri(result); 
        }

        public static Uri BuildSvnUrl(Uri uri)
        {
            var host = uri.GetComponents(UriComponents.Host, UriFormat.Unescaped);

            var projectName = host.Split('.')[0];

            var result = string.Format("https://{0}.svn.codeplex.com/svn", projectName);

            return new Uri(result);
        }

        public static Uri BuildSvnUrl(string projectName)
        {
            var result = string.Format("https://{0}.svn.codeplex.com/svn", projectName);

            return new Uri(result);
        }
    }
}
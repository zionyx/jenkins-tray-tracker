namespace JenkinsTray.Utils
{
    public static class NetUtils
    {
        public static string ConcatUrls(string fragment1, params string[] fragments)
        {
            var res = fragment1.TrimEnd('/');
            foreach (var fragment in fragments)
                res += "/" + fragment.TrimStart('/');
            return res;
        }

        public static string ConcatUrlsWithoutTrailingSlash(string fragment1, params string[] fragments)
        {
            var res = fragment1.TrimEnd('/');
            foreach (var fragment in fragments)
                res += fragment.TrimStart('/');
            return res;
        }
    }
}
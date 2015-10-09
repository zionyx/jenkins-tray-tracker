using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace JenkinsTray.Utils.IO
{
    public static class PathHelper
    {
        // returns
        //   - 'absoluteOrRelativePath' if 'absoluteOrRelativePath' is absolute
        //   - 'pathRoot\absoluteOrRelativePath' otherwise
        public static string GetPath(string absoluteOrRelativePath, string pathRoot)
        {
            if (absoluteOrRelativePath == null)
                return null;
            if (Path.IsPathRooted(absoluteOrRelativePath))
                return absoluteOrRelativePath;
            return Path.Combine(pathRoot, absoluteOrRelativePath);
        }

        public static string Combine(params string[] paths)
        {
            string res = null;
            foreach (string path in paths)
            {
                if (res == null)
                    res = path;
                else
                    res = Path.Combine(res, path);
            }
            return res;
        }

        public static string GetAbsolutePath(Assembly assembly)
        {
            string startupPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            Uri uri = new Uri(startupPath);
            return uri.LocalPath;
        }
    }
}

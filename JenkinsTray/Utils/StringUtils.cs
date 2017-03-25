using System.Collections.Generic;
using System.Text;

namespace JenkinsTray.Utils
{
    public static class StringUtils
    {
        public static string Join<T>(IEnumerable<T> objects, string separator)
            where T : class
        {
            var sep = "";
            var res = new StringBuilder();
            foreach (object o in objects)
            {
                res.Append(sep).Append(o);
                sep = separator;
            }
            return res.ToString();
        }

        // Extracts the user name from a string:
        // - "User Name" -> "User Name"
        // - "User Name <user@example.com>" -> "User Name"
        // - "User Name (user@example.com)" -> "User Name"
        public static string ExtractUserName(string fullName)
        {
            var res = fullName;

            var index = res.IndexOf(" <");
            if (index > 0)
                res = res.Substring(0, index);

            index = res.IndexOf(" (");
            if (index > 0)
                res = res.Substring(0, index);

            res = res.Trim();
            return res;
        }
    }
}
using System.Text.RegularExpressions;

namespace TeamLeasing.Infrastructure.Extension
{
    public static class StringExstension
    {
       public static string Between(this string source, string left, string right)
        {
            return Regex.Match(
                    source,
                    string.Format("{0}(.*){1}", left, right))
                .Groups[1].Value;
        }
    }
}
using System.Text.RegularExpressions;

namespace TeamLeasing.Infrastructure.Extension
{
    public static class StringExstension
    {
       public static string Between(this string source, string left, string right)
        {
            string FinalString;
            int Pos1 = source.IndexOf(left) + left.Length;
            int Pos2 = source.IndexOf(right);
            FinalString = source.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }
    }
}
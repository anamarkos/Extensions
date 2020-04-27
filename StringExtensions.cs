using System.Text.RegularExpressions;

namespace Extensions
{
    public static class StringExtensions
    {
        // Usage: var count = strCount.ToInt();
        public static int ToInt(this string input)
        {
            if (!int.TryParse(input, out int result))
            {
                result = 0;
            }
            return result;
        }

        // Usage: var count = "supercalifragilisticexpealidocious".Occurrence("li"); // returns 3
        public static int Occurrence(this string instr, string search)
        {
            return Regex.Matches(instr, search).Count;
        }
    }
}

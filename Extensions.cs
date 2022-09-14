namespace FlightPlanner
{
    internal static class Extensions
    {
        public static char FirstNonWhiteChar(this string s)
        {
            //If this string is empty, just return a whitespace character
            if (s.Length == 0) return ' ';

            for (int i = 0; i < s.Length; i++)
            {
                if (!char.IsWhiteSpace(s[i])) return s[i];
            }

            //If this string is all whitespace, just return the first whitespace character
            return s[0];
        }

        public static T Choice<T>(this Random random, IEnumerable<T> choices)
        {
            return choices.ElementAt(random.Next(0, choices.Count()));
        }
    }
}
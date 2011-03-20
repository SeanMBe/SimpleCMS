namespace SimpleCMS.Helpers
{
    public static class StringHelpers
    {
        public static string Ellipsify(this string text, int maxLength)
        {
            var result = text;

            if (!string.IsNullOrEmpty(text) && text.Length > maxLength)
            {
                result = text.Substring(0, maxLength);
                result += "...";
            }

            return result;
        }
    }
}
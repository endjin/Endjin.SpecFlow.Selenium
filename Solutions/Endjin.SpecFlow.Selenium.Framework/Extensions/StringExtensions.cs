namespace Endjin.SpecFlow.Selenium.Framework.Extensions
{
    public static class StringExtensions
    {
        public static bool? AsBool(this string value)
        {
            bool result;
            return bool.TryParse(value, out result) ? result : (bool?)null;
        }

        public static int? AsInt(this string value)
        {
            int result;
            return int.TryParse(value, out result) ? result : (int?)null;
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
    }
}
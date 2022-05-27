namespace SiteActivityReporting.API.Util
{
    public class Converter
    {
        public static int StringToNearestInt(string value)
        {
            decimal numValue = 0;
            bool parseResult = decimal.TryParse(value, out numValue);

            if (!parseResult)
            {
                return -1;
            }

            numValue = Math.Round(numValue);

            return (int)numValue;
        }
    }
}

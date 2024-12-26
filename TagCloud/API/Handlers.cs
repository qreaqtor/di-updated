using System.Drawing;

namespace TagCloud.API
{
    public static class Handlers
    {
        public static (bool, int) ParseInt(string input) => (int.TryParse(input, out var converted), converted);

        public static (bool, float) ParseFloat(string input) => (float.TryParse(input, out var converted), converted);

        public static (bool, string) GetPath(string input) => (File.Exists(input), input);

        public static (bool, Color) GetColorFromName(string input)
        {
            var color = Color.FromName(input);
            return (color.IsKnownColor, color);
        }

        public static (bool, string) GetFontFamilyName(string input) => (FontFamily.Families.Any(x => x.Name == input), input);

        public static (bool, TEnum) ParseEnum<TEnum>(string input)
        {
            if (Enum.TryParse(typeof(TEnum), input, true, out var enumValue))
                return (true, (TEnum)enumValue);

            return (false, default(TEnum));
        }
    }
}

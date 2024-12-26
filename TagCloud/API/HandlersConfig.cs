using System.Drawing;
using TagCloud.Config;
using TagCloud.ReadWriter;
using TagCloud.Models;

namespace TagCloud.API
{
    public static class HandlersConfig
    {
        public static Dictionary<string, Action<AppConfig, IReadWriter>> SetAppConfig
        {
            get
            {
                return new Dictionary<string, Action<AppConfig, IReadWriter>>()
                {
                    { "Да", SetDefaultConfig },
                    { "Нет", SetCustomConfig },
                };
            }
        }

        private static void SetDefaultConfig(AppConfig config, IReadWriter _)
        {
            config.CloudLayouterConfig = DefaultConfigs.DefaultCloudLayouterConfig;
            config.FontConfig = DefaultConfigs.DefaultFontConfig;
            config.ImageConfig = DefaultConfigs.DefaultImageConfig;
        }

        private static void SetCustomConfig(AppConfig config, IReadWriter readWriter)
        {
            config.CloudLayouterConfig = GetCustomCloudLayouterConfig(readWriter);
            config.FontConfig = GetCustomFontConfig(readWriter);
            config.ImageConfig = GetCustomImageConfig(readWriter);
        }

        private static ImageConfig GetCustomImageConfig(IReadWriter readWriter)
        {
            var width = readWriter.ReadLine(Messages.BeforeWidthInput, Messages.BadFormat, Handlers.ParseInt);

            var height = readWriter.ReadLine(Messages.BeforeHeightInput, Messages.BadFormat, Handlers.ParseInt);

            var background = readWriter.ReadLine(Messages.BeforeBackgroundInput, Messages.UnknownColor, Handlers.GetColorFromName);

            var countColors = readWriter.ReadLine(Messages.CountWordsColors, Messages.BadFormat, Handlers.ParseInt);

            var wordsColors = new Color[countColors];
            for (int i = 0; i< countColors; i++)
                wordsColors[i] = readWriter.ReadLine(Messages.BeforeColorInput, Messages.UnknownColor, Handlers.GetColorFromName);

            var colorScheme = readWriter.ReadLine(Messages.ColorSchemeInput, Messages.UnknownColorScheme, Handlers.ParseEnum<ColorScheme>);

            return new ImageConfig(width, height, background, wordsColors, colorScheme);
        }

        private static FontConfig GetCustomFontConfig(IReadWriter readWriter)
        {
            var fontFamily = readWriter.ReadLine(Messages.FontFamilyName, Messages.UnknownFontFamilyName, Handlers.GetFontFamilyName);

            var fontSize = readWriter.ReadLine(Messages.FontSize, Messages.BadFormat, Handlers.ParseFloat);

            var fontStyle = readWriter.ReadLine(Messages.FontStyle, Messages.UnknownFontStyle, Handlers.ParseEnum<FontStyle>);

            var increase = readWriter.ReadLine(Messages.FontIncrease, Messages.BadFormat, Handlers.ParseInt);

            return new FontConfig(fontFamily, fontSize, fontStyle, increase);
        }

        private static CloudLayouterConfig GetCustomCloudLayouterConfig(IReadWriter readWriter)
        {
            var radius = readWriter.ReadLine(Messages.BeforeRadiusInput, Messages.BadFormat, Handlers.ParseInt);

            var deltaRadius = readWriter.ReadLine(Messages.BeforeRadiusDeltaInput, Messages.BadFormat, Handlers.ParseInt);

            var angle = readWriter.ReadLine(Messages.BeforeAngleInput, Messages.BadFormat, Handlers.ParseInt);

            var deltaAngle = readWriter.ReadLine(Messages.BeforeAngleDeltaInput, Messages.BadFormat, Handlers.ParseInt);

            return new CloudLayouterConfig(radius, deltaRadius, angle, deltaAngle);
        }
    }
}

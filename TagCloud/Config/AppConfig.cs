using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Config
{
    public class AppConfig
    {
        public CloudLayouterConfig CloudLayouterConfig { get; set; }

        public FontConfig FontConfig { get; set; }

        public ImageConfig ImageConfig { get; set; }

        private Dictionary<ColorScheme, Func<Color>> ColorSchemas;

        private Random rnd;

        private int index;

        public AppConfig() 
        {
            CloudLayouterConfig = DefaultConfigs.DefaultCloudLayouterConfig;

            FontConfig = DefaultConfigs.DefaultFontConfig;

            ImageConfig = DefaultConfigs.DefaultImageConfig;

            rnd = new Random();
            index = -1;

            ColorSchemas = new Dictionary<ColorScheme, Func<Color>>
            {
                {ColorScheme.Random,  ColorSchemeRandom},
                {ColorScheme.RoundRobin, ColorSchemeRoundRobin},
            };
        }

        public Color GetNextColor() => ColorSchemas[ImageConfig.ColorScheme]();

        private Color ColorSchemeRandom()
        {
            var index = rnd.Next(0, ImageConfig.WordColors.Length);

            return ImageConfig.WordColors[index];
        }

        private Color ColorSchemeRoundRobin()
        {
            index = (index + 1) % ImageConfig.WordColors.Length;

            return ImageConfig.WordColors[index];
        }
    }
}

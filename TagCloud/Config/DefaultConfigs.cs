using System.Drawing;
using TagCloud.Models;

namespace TagCloud.Config
{
    public static class DefaultConfigs
    {
        public static ImageConfig DefaultImageConfig
        {
            get { return new ImageConfig(800, 800, Color.Black, [Color.Red, Color.Blue, Color.Green, Color.White], ColorScheme.RoundRobin); }
        }

        public static CloudLayouterConfig DefaultCloudLayouterConfig
        {
            get { return new CloudLayouterConfig(2, 5, 2, 5); }
        }

        public static FontConfig DefaultFontConfig
        {
            get { return new FontConfig("GenericSerif", 14, FontStyle.Regular, 5); }
        }
    }
}

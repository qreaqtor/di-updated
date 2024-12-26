using System.Drawing;

namespace TagCloud.Models
{
    public record struct ImageConfig(int Width, int Height, Color Background, Color[] WordColors, ColorScheme ColorScheme);
}

using System.Drawing;

namespace TagCloud.Models
{
    public record struct FontConfig(string FontFamily, float FontSize, FontStyle Style, int FontIncreaseByWordLevel);
}

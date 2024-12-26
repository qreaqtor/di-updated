using System.Drawing;

namespace TagCloud.Models
{
    public record struct WordTag(RectangleF Rectangle, string Content, Font Font);
}

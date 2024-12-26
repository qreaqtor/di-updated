using System.Drawing;
using TagCloud.Models;

namespace TagCloud.CircularCloudLayouter
{
    public interface ICloudLayouter
    {
        RectangleF GetPossibleNextRectangle(IEnumerable<WordTag> cloudRectangles, SizeF rectangleSize);
    }
}

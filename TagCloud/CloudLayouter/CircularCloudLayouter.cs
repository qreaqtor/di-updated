using System.Drawing;
using TagCloud.Config;
using TagCloud.Models;

namespace TagCloud.CircularCloudLayouter
{
    public class CircularCloudLayouter :ICloudLayouter
    {
        private AppConfig appConfig;

        public CircularCloudLayouter(AppConfig appConfig) 
        {
            this.appConfig = appConfig;
        }

        public RectangleF GetPossibleNextRectangle(IEnumerable<WordTag> cloudRectangles, SizeF rectangleSize)
        {
            if (rectangleSize.Width <= 0 || rectangleSize.Height <= 0)
                throw new ArgumentException("the width and height of the rectangle must be positive numbers");

            return FindPossibleNextRectangle(cloudRectangles, rectangleSize);
        }

        private RectangleF FindPossibleNextRectangle(IEnumerable<WordTag> cloudRectangles, SizeF rectangleSize)
        {
            var cloudLayouterConfig = appConfig.CloudLayouterConfig;
            var imageConfig = appConfig.ImageConfig;

            var center = new Point(imageConfig.Width / 2, imageConfig.Height / 2);

            var radius = cloudLayouterConfig.Radius;
            var angle = cloudLayouterConfig.Angle;

            while (true)
            {
                var point = new Point(
                    (int)(center.X + radius * Math.Cos(angle)),
                    (int)(center.Y + radius * Math.Sin(angle))
                );

                var possibleRectangle = new RectangleF(point, rectangleSize);

                if (!cloudRectangles.Any(textRectangle => textRectangle.Rectangle.IntersectsWith(possibleRectangle)))
                    return possibleRectangle;

                angle += cloudLayouterConfig.DeltaAngle;
                radius += cloudLayouterConfig.DeltaRadius;
            }
        }
    }
}

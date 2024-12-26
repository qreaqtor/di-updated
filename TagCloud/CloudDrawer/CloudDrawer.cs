using System.Drawing;
using TagCloud.API;
using TagCloud.Config;
using TagCloud.Models;

namespace TagCloud.CloudDrawer
{
    public class CloudDrawer : ICloudDrawer
    {
        private readonly AppConfig appConfig;

        public CloudDrawer(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }

        public void DrawWordsAndSave(IEnumerable<WordTag> words)
        {
            var imageConfig = appConfig.ImageConfig;

            var bitmap = new Bitmap(imageConfig.Width, imageConfig.Height);

            var graphics = Graphics.FromImage(bitmap);

            graphics.Clear(imageConfig.Background);

            var index = 0;

            foreach (var word in words)
            {
                var brush = new SolidBrush(imageConfig.WordColors[index % imageConfig.WordColors.Length]);
                graphics.DrawString(word.Content, word.Font, brush, word.Rectangle);
                index++;
            }

            bitmap.Save(Messages.Filename);
        }
    }
}

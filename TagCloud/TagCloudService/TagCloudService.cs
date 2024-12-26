using System.Drawing;
using TagCloud.CircularCloudLayouter;
using TagCloud.Config;
using TagCloud.Models;
using TagCloud.TagCloudService;
using TagCloud.WordsProcessor;

namespace TagCloud.Service
{
    public class TagCloudService : ITagCloudService
    {
        private readonly ICloudLayouter cloudLayouter;
        private readonly IWordProcessor wordProcessor;

        private readonly AppConfig appConfig;

        public TagCloudService(ICloudLayouter cloudLayouter, IWordProcessor wordProcessor, AppConfig appConfig) 
        {
            this.cloudLayouter = cloudLayouter;
            this.wordProcessor = wordProcessor;

            this.appConfig = appConfig;
        }

        public IEnumerable<WordTag> GetWordTags(IEnumerable<string> wordsData, IEnumerable<string> boringWords)
        {
            var imageConfig = appConfig.ImageConfig;
            var fontConfig = appConfig.FontConfig;

            var rectangles = new List<WordTag>();

            var words = wordProcessor.GetProcessedData(wordsData, boringWords);

            var bitmap = new Bitmap(imageConfig.Width, imageConfig.Height);

            var graphics = Graphics.FromImage(bitmap);

            foreach (var word in words)
            {
                var font = new Font(fontConfig.FontFamily, word.FontSize, fontConfig.Style);

                var textSize = graphics.MeasureString(word.Content, font);

                var rectangle = cloudLayouter.GetPossibleNextRectangle(rectangles, textSize);

                rectangles.Add(new WordTag(rectangle, word.Content, font));
            }

            return rectangles;
        }
    }
}

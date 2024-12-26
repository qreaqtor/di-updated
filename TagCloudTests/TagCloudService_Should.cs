using FluentAssertions;
using TagCloud.CircularCloudLayouter;
using TagCloud.Config;
using TagCloud.Models;
using TagCloud.Service;
using TagCloud.WordsProcessor;

namespace TagCloudTests
{
    [TestFixture]
    public class TagCloudService_Should
    {
        private TagCloudService tagCloudService;

        [SetUp]
        public void SetUp()
        {
            var appConfig = new AppConfig();
            var cloudLayouter = new CircularCloudLayouter(appConfig);
            var wordProcessor = new WordProcessor(appConfig);

            tagCloudService = new TagCloudService(cloudLayouter, wordProcessor, appConfig);
        }

        [Test]
        public void CreateAnEmptyLayout_WhenAllDataIsEmpty()
        {
            var result = tagCloudService.GetWordTags([], []);

            result.Should().BeEmpty();
        }

        [Test]
        public void CreateLayoutWithoutIntersections_WhenNoRepeatedWords()
        {
            var wordsData = new List<string>()
            {
                "яблоко",
                "банан",
                "груша",
            };

            var boringData = new List<string>();

            var wordTags = tagCloudService.GetWordTags(wordsData, boringData);

            IsIntersections(wordTags).Should().BeFalse();
        }

        [Test]
        public void CreateLayoutWithoutIntersections_WithRepeatedWords()
        {
            var wordsData = new List<string>()
            {
                "яблоко",
                "банан",
                "груша",
                "яблоко",
                "банан",
                "яблоко"
            };

            var boringData = new List<string>();

            var wordTags = tagCloudService.GetWordTags(wordsData, boringData);

            IsIntersections(wordTags).Should().BeFalse();
        }

        public bool IsIntersections(IEnumerable<WordTag> wordTags)
        {
            var rectangles = wordTags.Select(x => x.Rectangle).ToList();

            for (int i = 0; i < rectangles.Count; i++)
                for (int j = i + 1; j < rectangles.Count; j++)
                    if (rectangles[i].IntersectsWith(rectangles[j]))
                        return true;

            return false;
        }
    }
}

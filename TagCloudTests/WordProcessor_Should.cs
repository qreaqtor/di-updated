using FluentAssertions;
using TagCloud.Config;
using TagCloud.Models;
using TagCloud.WordsProcessor;

namespace TagCloudTests
{
    [TestFixture]
    public class WordProcessor_Should
    {
        private WordProcessor wordProcessor;
        private AppConfig appConfig;

        [SetUp]
        public void SetUp()
        {
            appConfig = new AppConfig();

            wordProcessor = new WordProcessor(appConfig);
        }

        [Test]
        public void GetProcessedData_WhenFileWithBoringWordsIsEmpty()
        {
            var wordsData = new List<string>()
            {
                "€блоко",
                "банан",
                "груша",
            };

            var boringData = new List<string>();

            var processedData = wordProcessor.GetProcessedData(wordsData, boringData);

            processedData.Should().HaveCount(wordsData.Count);
        }

        [Test]
        public void GetProcessedData_WhenFileWithBoringWordsIsNotEmpty()
        {
            var wordsData = new List<string>()
            {
                "€блоко",
                "банан",
                "груша",
            };

            var boringData = new List<string>()
            {
                "€блоко",
                "арбуз",
            };

            var processedData = wordProcessor.GetProcessedData(wordsData, boringData);

            processedData.Should().NotContain(word => boringData.Contains(word.Content));
        }

        [Test]
        public void GetProcessedData_CorrectCalculateWordSizeLevel()
        {
            var wordsData = new List<string>()
            {
                "€блоко",
                "банан",
                "груша",
                "€блоко",
                "банан",
                "€блоко",
                "€блоко",
                "€блоко",
            };

            var boringData = new List<string>();

            var fontSize = appConfig.FontConfig.FontSize;
            var increase = appConfig.FontConfig.FontIncreaseByWordLevel;

            var expectedData = new List<Word>()
            {
                new( "груша", fontSize),
                new( "банан", fontSize + increase),
                new( "€блоко", fontSize + 2 * increase),
            };

            var processedData = wordProcessor.GetProcessedData(wordsData, boringData);

            processedData.Should().BeEquivalentTo(expectedData);
        }

        [Test]
        public void GetProcessedData_InDescendingOrder()
        {
            var wordsData = new List<string>()
            {
                "€блоко",
                "банан",
                "груша",
                "€блоко",
                "банан",
                "€блоко",
                "€блоко",
                "€блоко",
            };

            var boringData = new List<string>();

            var fontSize = appConfig.FontConfig.FontSize;
            var increase = appConfig.FontConfig.FontIncreaseByWordLevel;

            var processedData = wordProcessor.GetProcessedData(wordsData, boringData);

            processedData.Should().BeInDescendingOrder(word => word.FontSize);
        }

        [Test]
        public void ReturnEmptyProcessedData_WhenSourceWordFileIsEmpty()
        {
            var wordsData = new List<string>();

            var boringData = new List<string>();

            var processedData = wordProcessor.GetProcessedData(wordsData, boringData);

            processedData.Should().BeEmpty();
        }
    }
}
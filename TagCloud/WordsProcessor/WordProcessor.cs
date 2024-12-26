using TagCloud.Config;
using TagCloud.Models;

namespace TagCloud.WordsProcessor
{
    public class WordProcessor : IWordProcessor
    {
        private readonly AppConfig appConfig;

        public WordProcessor(AppConfig appConfig)
        {
            this.appConfig = appConfig;
        }

        public IEnumerable<Word> GetProcessedData(IEnumerable<string> wordsData, IEnumerable<string> boringWords)
        {
            var cache = new Dictionary<string, int>();

            var succesWords = wordsData.Where(word => !boringWords.Contains(word));

            foreach (var word in succesWords)
            {
                if (!cache.ContainsKey(word))
                    cache[word] = 0;

                cache[word]++;
            }

            var sortedWords = cache.OrderBy(wordCountPair => cache[wordCountPair.Key]);

            var prevCount = sortedWords.LastOrDefault().Value;
            var level = 0;

            var stack = new Stack<Word>();

            foreach (var (word, count) in sortedWords)
            {
                if (count - prevCount > 0)
                    level++;

                stack.Push(new Word(word, CalculateFontSize(level)));

                prevCount = count;
            }

            return stack;
        }

        private float CalculateFontSize(int sizeLevel)
        {
            var fontIncreaseByWordLevel = appConfig.FontConfig.FontIncreaseByWordLevel;
            var defaultSize = appConfig.FontConfig.FontSize;

            return defaultSize + sizeLevel * fontIncreaseByWordLevel;
        }
    }
}

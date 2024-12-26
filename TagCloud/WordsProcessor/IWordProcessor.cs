using TagCloud.Models;

namespace TagCloud.WordsProcessor
{
    public interface IWordProcessor
    {
        IEnumerable<Word> GetProcessedData(IEnumerable<string> wordsData, IEnumerable<string> boringWords);
    }
}

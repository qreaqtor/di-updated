namespace TagCloud.ReadWriter
{
    public interface IReadWriter
    {
        IEnumerable<string> ReadDataFromFile(string path);

        TOut ReadLine<TOut>(string beforeInputMsg, string badInputMsg, Func<string, (bool, TOut)> check);

        void WriteLine(string msg);
    }
}

namespace TagCloud.ReadWriter
{
    public class ConsoleReadWriter : IReadWriter
    {
        public IEnumerable<string> ReadDataFromFile(string path) => File.ReadAllLines(path).Select(line => line.Trim().ToLower());

        public TOut ReadLine<TOut>(string beforeInput, string badInput, Func<string, (bool, TOut)> check)
        {
            while (true)
            {
                Console.WriteLine(beforeInput);

                var input = Console.ReadLine().Trim();

                var (ok, converted) = check(input);

                if (ok)
                    return converted;

                Console.WriteLine(badInput);
            }
        }

        public void WriteLine(string input) => Console.WriteLine(input);
    }
}

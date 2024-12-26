using TagCloud.CloudDrawer;
using TagCloud.Config;
using TagCloud.ReadWriter;
using TagCloud.TagCloudService;

namespace TagCloud.API
{
    public class ConsoleAPI
    {
        private readonly IReadWriter readWriter;
        private readonly ICloudDrawer drawer;
        private readonly ITagCloudService tagCloudService;

        private AppConfig appConfig;

        public ConsoleAPI(IReadWriter reader, ICloudDrawer drawer, ITagCloudService tagCloudService, AppConfig appConfig)
        {
            this.readWriter = reader;
            this.drawer = drawer;
            this.tagCloudService = tagCloudService;

            this.appConfig = appConfig;
        }

        public void Start()
        {
            var wordsPath = readWriter.ReadLine(Messages.BeforeWordsDataInput, Messages.FileNotFound, Handlers.GetPath);
            var boringPath = readWriter.ReadLine(Messages.BeforeBoringWordsDataInput, Messages.FileNotFound, Handlers.GetPath);

            var set = readWriter.ReadLine(Messages.UseDefaultConfig, Messages.BadFormat, 
                x => (HandlersConfig.SetAppConfig.TryGetValue(x, out var set), set));

            set(appConfig, readWriter);

            var wordsData = readWriter.ReadDataFromFile(wordsPath);
            var boringWordsData = readWriter.ReadDataFromFile(boringPath);

            var words = tagCloudService.GetWordTags(wordsData, boringWordsData);

            drawer.DrawWordsAndSave(words);

            readWriter.WriteLine(Messages.Success);
        }
    }
}

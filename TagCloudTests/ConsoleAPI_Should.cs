using FakeItEasy;
using FluentAssertions;
using System.Drawing;
using TagCloud.API;
using TagCloud.CircularCloudLayouter;
using TagCloud.CloudDrawer;
using TagCloud.Config;
using TagCloud.Models;
using TagCloud.ReadWriter;
using TagCloud.Service;
using TagCloud.TagCloudService;
using TagCloud.WordsProcessor;

namespace TagCloudTests
{
    [TestFixture]
    public class ConsoleAPI_Should
    {
        private AppConfig appConfig;

        [SetUp]
        public void SetUp()
        {
            appConfig = new AppConfig();
        }

        [Test]
        public void ConsoleAPI_UseDefaultAppConfigTest()
        {
            var readerMock = A.Fake<IReadWriter>();
            var drawerMock = A.Fake<ICloudDrawer>();
            var tagCloudServiceMock = A.Fake<ITagCloudService>();

            Func<string, (bool, Action<AppConfig, IReadWriter>)> configCheck = 
                x => (HandlersConfig.SetAppConfig.TryGetValue(x, out var set), set);

            A.CallTo(() => readerMock.ReadLine<string>(null, null, null)).WithAnyArguments().Returns(string.Empty);
            A.CallTo(() => readerMock.ReadLine(Messages.UseDefaultConfig, Messages.BadFormat, configCheck)).Returns(HandlersConfig.SetAppConfig["Да"]);

            var appConfig = new AppConfig();

            var api = new ConsoleAPI(readerMock, drawerMock, tagCloudServiceMock, appConfig);

            api.Start();

            var defaultAppConfig = new AppConfig();

            appConfig.Should().BeEquivalentTo(defaultAppConfig);
        }
    }
}

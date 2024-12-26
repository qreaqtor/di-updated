using Autofac;
using TagCloud.API;
using TagCloud.CircularCloudLayouter;
using TagCloud.CloudDrawer;
using TagCloud.Config;
using TagCloud.ReadWriter;
using TagCloud.Service;
using TagCloud.TagCloudService;
using TagCloud.WordsProcessor;
using static System.Formats.Asn1.AsnWriter;

var builder = new ContainerBuilder();

builder.RegisterType<AppConfig>().AsSelf().SingleInstance();

builder.RegisterType<ConsoleReadWriter>().As<IReadWriter>().SingleInstance();
builder.RegisterType<WordProcessor>().As<IWordProcessor>().SingleInstance();
builder.RegisterType<CircularCloudLayouter>().As<ICloudLayouter>().SingleInstance();
builder.RegisterType<TagCloudService>().As<ITagCloudService>().SingleInstance();
builder.RegisterType<CloudDrawer>().As<ICloudDrawer>().SingleInstance();

builder.RegisterType<ConsoleAPI>().AsSelf().SingleInstance();

var container = builder.Build();

var api = container.Resolve<ConsoleAPI>();
api.Start();
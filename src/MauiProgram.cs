using Microsoft.Extensions.Logging;
using PristonToolsEU.BossTiming;
using PristonToolsEU.ServerTiming;
using PristonToolsEU.Networking;

namespace PristonToolsEU;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<IRestClient, RestClient>();
		builder.Services.AddSingleton<IServerTime, ServerTime>();
		builder.Services.AddSingleton<IBossTimePropsReader, BossTimePropsReader>();
		builder.Services.AddSingleton<IBossTimer, BossTimer>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<MainPage>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

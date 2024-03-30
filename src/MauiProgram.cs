using Microsoft.Extensions.Logging;
using PristonToolsEU.Alarming;
using PristonToolsEU.BossTiming;
using PristonToolsEU.Logging;
using PristonToolsEU.ServerTiming;
using PristonToolsEU.Networking;
using LogLevel = PristonToolsEU.Logging.LogLevel;

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

		Log.Instance.AddLogTarget(new ConsoleLogTarget());
		Log.Instance.LogLevel = LogLevel.Debug;
		
		builder.Services.AddSingleton<IRestClient, RestClient>();
		builder.Services.AddSingleton<IServerTime, ServerTime>();
		builder.Services.AddSingleton<IBossReader, BossReader>();
		builder.Services.AddSingleton<IBossTimer, BossTimer>();
		builder.Services.AddSingleton<IAlarm, Alarm>();
		builder.Services.AddSingleton<MainPageViewModel>();
		builder.Services.AddSingleton<MainPage>();
#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

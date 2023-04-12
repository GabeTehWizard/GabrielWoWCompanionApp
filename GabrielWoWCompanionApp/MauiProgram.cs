using Microsoft.Extensions.Logging;
using GabrielWoWCompanionApp.View;
using CommunityToolkit.Maui;
using GabrielWoWCompanionApp.Services;

namespace GabrielWoWCompanionApp;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("BreatheFireIv-3zAVZ.ttf", "BreatheFire");
                fonts.AddFont("WarriorOfWorld.ttf", "WarriorOfWorld");
                fonts.AddFont("burning_wrath.ttf", "Wrath");
                fonts.AddFont("SchibstedGrotesk-Bold.ttf", "Schib_Bold");
                fonts.AddFont("SchibstedGrotesk-Semibold.ttf", "Schib_Semibold");
                fonts.AddFont("SchibstedGrotesk-Medium.ttf", "Schib_Medium");
                fonts.AddFont("SchibstedGrotesk-Regular.ttf", "Schib_Regular");
            });

#if DEBUG
		builder.Logging.AddDebug();
#endif
        // Services
        builder.Services.AddSingleton<WarcraftLogsService>();
        builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);

        // View Models
        builder.Services.AddSingleton<MainPageViewModel>();
        builder.Services.AddTransient<DisciplineTalentsPageViewModel>();
        builder.Services.AddTransient<HolyTalentsPageViewModel>();
        builder.Services.AddTransient<LogPageViewModel>();
        builder.Services.AddTransient<ShadowTalentsPageViewModel>();
        builder.Services.AddTransient<TalentsPageViewModel>();

        // Pages
        builder.Services.AddSingleton<MainPage>();
        builder.Services.AddTransient<DisciplineTalentsPage>();
        builder.Services.AddTransient<HolyTalentsPage>();
        builder.Services.AddTransient<LogPage>();
        builder.Services.AddTransient<ShadowTalentsPage>();
        builder.Services.AddTransient<TalentsPage>();

        return builder.Build();
	}
}

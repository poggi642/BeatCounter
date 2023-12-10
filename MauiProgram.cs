using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace BeatCounter;
public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkitMediaElement()
            // After initializing the .NET MAUI Community Toolkit, optionally add additional fonts
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
//#if IOS
//        AVAudioSession.SharedInstance().SetActive(true);
//        AVAudioSession.SharedInstance().SetCategory(AVAudioSessionCategory.Playback);
//#endif

        return builder.Build();
	}
}

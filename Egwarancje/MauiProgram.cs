using CommunityToolkit.Maui;
using Egwarancje.ViewModels;
using Egwarancje.Views;
using Microsoft.Extensions.Logging;

namespace Egwarancje;

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
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        //xmlns:toolkit="http://schemas.microsoft.com/dotnet/2022/maui/toolkit"
        builder.Services.AddSingleton<LoginView>();
        builder.Services.AddSingleton<LoginViewModel>();


        return builder.Build();
    }
}

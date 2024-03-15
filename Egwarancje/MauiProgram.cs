using CommunityToolkit.Maui;
using Egwarancje.Context;
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

        builder.Services.AddDbContext<LocalDatabaseContext>();

        builder.Services.AddTransient<LoginView>();
        builder.Services.AddTransient<LoginViewModel>();

        builder.Services.AddTransient<RegisterView>();
        builder.Services.AddTransient<RegisterViewModel>();

        builder.Services.AddTransient<RecoverPasswordView>();
        builder.Services.AddTransient<RecoverPasswordViewModel>();

        builder.Services.AddTransient<OrderPanelView>();
        builder.Services.AddTransient<OrderPanelViewModel>();

        builder.Services.AddTransient<UserProfileView>();
        builder.Services.AddTransient<UserProfileViewModel>();

        return builder.Build();
    }
}

﻿using CommunityToolkit.Maui;
using Egwarancje.ViewModels.Auth;
using Egwarancje.ViewModels.Warranties;
using Egwarancje.ViewModels.Orders;
using Egwarancje.ViewModels.ProfileDetails;
using Egwarancje.Views.Auth;
using Egwarancje.Views.Warranties;
using Egwarancje.Views.Orders;
using Egwarancje.Views.ProfileDetails;
using Microsoft.Extensions.Logging;
using Mopups.Hosting;
using Egwarancje.Services;

namespace Egwarancje;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureMopups()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("BalooBhai-Regular.ttf", "BalooBhaiRegular");
                fonts.AddFont("BakbakOne-Regular.ttf", "BakbakOneRegular");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        builder.Services.AddSingleton<UserService>();
        
        builder.Services.AddTransient<HomeView>();
        builder.Services.AddTransient<HomeViewModel>();

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

        builder.Services.AddTransient<WarrantyPanelView>();
        builder.Services.AddTransient<WarrantyPanelViewModel>();

        builder.Services.AddTransient<ConfiguratorView>();
        builder.Services.AddTransient<ConfiguratorViewModel>();

        builder.Services.AddTransient<CartView>();
        builder.Services.AddTransient<CartViewModel>();

        builder.Services.AddTransient<ProductConfigurationView>();
        builder.Services.AddTransient<ProductConfigurationViewModel>();

        return builder.Build();
    }
}

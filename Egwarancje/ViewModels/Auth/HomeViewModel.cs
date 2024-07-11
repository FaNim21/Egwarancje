using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;


namespace Egwarancje.ViewModels.Auth;

public partial class HomeViewModel : BaseViewModel
{
    private readonly UserService _service;
    [ObservableProperty] private bool logging;

    public HomeViewModel(UserService service)
    {
        _service = service;
        Task.Factory.StartNew(LoadRememberMe);
    }

    private async Task LoadRememberMe()
    {
        bool rememberMe = Preferences.Get("RememberMe", false);
        if (rememberMe)
        {
            Logging = true;
            string email = Preferences.Get("Email", string.Empty);
            string password = Preferences.Get("Password", string.Empty);
            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
            {
                MessageResponse response = await _service.LoginAsync(email, password);
                if (response.Success)
                {
                    Application.Current?.Dispatcher.Dispatch(async delegate
                    {
                        await Shell.Current.GoToAsync("///MainTab//OrderPanel");
                    });
                }
                else
                {
                    Logging = false;
                    await Application.Current!.MainPage!.DisplayAlert("Message", response.Message, "OK");
                }

            }
        }
    }

    [RelayCommand]
    public async Task Register()
    {
        if (logging) return;
        await Shell.Current.GoToAsync("///Register");
    }

    [RelayCommand]
    public async Task Login()
    {
        if (logging) return;
        await Shell.Current.GoToAsync("///Login");
    }

}

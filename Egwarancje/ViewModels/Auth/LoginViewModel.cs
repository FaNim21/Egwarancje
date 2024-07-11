using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;

namespace Egwarancje.ViewModels.Auth;

public partial class LoginViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty] private string? email;
    [ObservableProperty] private string? password;

    [ObservableProperty] private bool rememberMe;


    public LoginViewModel(UserService service)
    {
        this.service = service;
    }

    [RelayCommand]
    public async Task Login()
    {
        if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Uzupełnij dane logowania", "OK");
            return;
        }

        MessageResponse reponse = await service.LoginAsync(Email, Password);
        if (!reponse.Success)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", reponse.Message, "OK");
            return;
        }

        if (RememberMe)
        {
            Preferences.Set("RememberMe", true);
            Preferences.Set("Email", Email);
            Preferences.Set("Password", Password);
        }
        else
        {
            Preferences.Set("RememberMe", false);
            Preferences.Remove("email");
            Preferences.Remove("password");
        }

        await Shell.Current.GoToAsync("///MainTab//OrderPanel");
    }

    [RelayCommand]
    public async Task Register()
    {
        await Shell.Current.GoToAsync("///Register");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Home");
    }

    [RelayCommand]
    public async Task ResetPassword()
    {
        await Shell.Current.GoToAsync("///Recover");
    }
}

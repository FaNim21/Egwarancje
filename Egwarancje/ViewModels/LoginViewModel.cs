using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;

namespace Egwarancje.ViewModels;

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

        bool success = await service.LoginAsync(Email, Password);
        if (!success)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Nieprawidłowe dane logowania", "OK");
            return;
        }

        await Shell.Current.GoToAsync("///MainTab//OrderPanel");
    }

    [RelayCommand]
    public async Task Register()
    {
        await Shell.Current.GoToAsync("///Register");
    }

    [RelayCommand]
    public async Task ResetPassword()
    {
        await Shell.Current.GoToAsync("///Recover");
    }
}

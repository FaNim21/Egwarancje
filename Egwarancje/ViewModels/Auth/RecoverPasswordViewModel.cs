using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;

namespace Egwarancje.ViewModels.Auth;

public partial class RecoverPasswordViewModel : BaseViewModel
{
    private readonly UserService _userService;

    [ObservableProperty]
    private string email;


    public RecoverPasswordViewModel(UserService userService)
    {
        _userService = userService;
        email = string.Empty;
    }

    [RelayCommand]
    public async Task RecoverPassword()
    {
        if (string.IsNullOrEmpty(Email))
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Nie wpisano email'a","OK");
        }

        MessageResponse response = await _userService.RestartPassword(Email);
        await Application.Current!.MainPage!.DisplayAlert("Message", response.Message, "OK");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}

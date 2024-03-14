using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels;

public partial class LoginViewModel : BaseViewModel
{
    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string password;


    public LoginViewModel()
    {

    }

    [RelayCommand]
    public async Task Login()
    {
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

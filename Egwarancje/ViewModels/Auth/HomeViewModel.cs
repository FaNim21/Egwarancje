using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels.Auth;

public partial class HomeViewModel : BaseViewModel
{
    public HomeViewModel()
    {
    }
    
    [RelayCommand]
    public async Task Register()
    {
        await Shell.Current.GoToAsync("///Register");
    }

    [RelayCommand]
    public async Task Login()
    {
        await Shell.Current.GoToAsync("///Login");
    }

}

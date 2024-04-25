using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels;

public partial class UserProfileViewModel : BaseViewModel
{
    [ObservableProperty]
    private string? namesurname;

    [ObservableProperty]
    private string? email;

    [ObservableProperty]
    private string? phone;

    public UserProfileViewModel()
    {
        
    }

    [RelayCommand]
    public async Task Logout()
    {
        await Shell.Current.GoToAsync("///Login");
    }

    [RelayCommand]
    public async Task ResetPassword()
    {
        await Shell.Current.GoToAsync("///Recover");
    }
}

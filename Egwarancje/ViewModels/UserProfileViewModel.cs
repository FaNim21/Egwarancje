using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels;

public partial class UserProfileViewModel : BaseViewModel
{


    public UserProfileViewModel()
    {
        
    }

    [RelayCommand]
    public async Task Logout()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}

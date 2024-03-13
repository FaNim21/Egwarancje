using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels;

public partial class RecoverPasswordViewModel : BaseViewModel
{
    [ObservableProperty]
    private string email;


    public RecoverPasswordViewModel()
    {
        
    }

    [RelayCommand]
    public async Task RecoverPassword()
    {
        return;
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}

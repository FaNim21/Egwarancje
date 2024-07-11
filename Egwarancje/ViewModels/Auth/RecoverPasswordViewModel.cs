using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels.Auth;

public partial class RecoverPasswordViewModel : BaseViewModel
{
    [ObservableProperty]
    private string email;


    public RecoverPasswordViewModel()
    {
        email = string.Empty;
    }

    [RelayCommand]
    public async Task RecoverPassword()
    {
        await Application.Current!.MainPage!.DisplayAlert("Message", $"Wysłano link do zmiany hasla na adres: {Email}", "OK");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Login");
    }
}

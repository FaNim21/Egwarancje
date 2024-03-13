using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Egwarancje.ViewModels;

public partial class RegisterViewModel : BaseViewModel
{
    [ObservableProperty]
    private string name;

    [ObservableProperty]
    private string email;

    [ObservableProperty]
    private string phoneNumber;

    [ObservableProperty]
    private string password;

    [ObservableProperty]
    private string passwordAgain;


    public RegisterViewModel()
    {
        
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///Login");
    }

    [RelayCommand]
    public async Task FinalizeRegister()
    {
        return;
        //TODO: 0 Tu bedzie walidacja do rejestrowania wraz z zatwierdzaniem

        await Shell.Current.GoToAsync("///Login");
    }
}

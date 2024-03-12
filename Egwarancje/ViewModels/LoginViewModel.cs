using CommunityToolkit.Mvvm.ComponentModel;

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

    public void Login()
    {

    }
}

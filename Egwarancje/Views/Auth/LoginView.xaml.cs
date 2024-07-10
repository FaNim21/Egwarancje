using Egwarancje.Utils;
using Egwarancje.ViewModels.Auth;

namespace Egwarancje.Views.Auth;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
using Egwarancje.Utils;
using Egwarancje.ViewModels.Auth;

namespace Egwarancje.Views;

public partial class LoginView : ContentPage
{
    public LoginView(LoginViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        versionLabel.Text = Consts.Version;
    }
}
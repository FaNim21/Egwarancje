using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class UserProfileView : ContentPage
{
    public UserProfileView(UserProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
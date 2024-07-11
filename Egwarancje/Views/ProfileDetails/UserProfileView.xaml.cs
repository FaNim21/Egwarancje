using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class UserProfileView : ContentPage
{
    public UserProfileView(UserProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
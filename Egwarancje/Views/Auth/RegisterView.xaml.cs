using Egwarancje.ViewModels.Auth;

namespace Egwarancje.Views.Auth;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
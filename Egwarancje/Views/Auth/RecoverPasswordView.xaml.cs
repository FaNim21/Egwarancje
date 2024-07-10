using Egwarancje.ViewModels.Auth;

namespace Egwarancje.Views.Auth;

public partial class RecoverPasswordView : ContentPage
{
	public RecoverPasswordView(RecoverPasswordViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
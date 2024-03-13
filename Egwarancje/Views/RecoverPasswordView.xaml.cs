using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class RecoverPasswordView : ContentPage
{
	public RecoverPasswordView(RecoverPasswordViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
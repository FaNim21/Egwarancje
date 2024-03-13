using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class RegisterView : ContentPage
{
	public RegisterView(RegisterViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
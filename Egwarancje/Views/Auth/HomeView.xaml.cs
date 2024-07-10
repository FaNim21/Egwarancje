using Egwarancje.ViewModels.Auth;

namespace Egwarancje.Views.Auth;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
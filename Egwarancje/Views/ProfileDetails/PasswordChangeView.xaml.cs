using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class PasswordChangeView
{
	public PasswordChangeView(PasswordChangeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
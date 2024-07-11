using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class AddressView
{
	public AddressView(AddressViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
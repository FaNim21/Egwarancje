using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class AddressDetailsView
{
	public AddressDetailsView(AddressDetailsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
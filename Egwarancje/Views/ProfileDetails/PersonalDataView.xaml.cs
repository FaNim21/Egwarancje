using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class PersonalDataView
{
	public PersonalDataView(PersonalDataViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
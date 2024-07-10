using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views;

public partial class PersonalDataView
{
	public PersonalDataView(PersonalDataViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
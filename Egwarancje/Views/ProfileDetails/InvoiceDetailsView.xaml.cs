using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class InvoiceDetailsView
{
	public InvoiceDetailsView(InvoiceDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
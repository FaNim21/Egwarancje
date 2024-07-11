using Egwarancje.ViewModels.ProfileDetails;

namespace Egwarancje.Views.ProfileDetails;

public partial class PaymentView
{
	public PaymentView(PaymentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
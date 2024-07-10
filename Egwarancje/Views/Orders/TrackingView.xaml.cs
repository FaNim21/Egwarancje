using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views.Orders;

public partial class TrackingView : ContentPage
{
	public TrackingView(TrackingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
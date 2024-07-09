using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views;

public partial class TrackingView : ContentPage
{
	public TrackingView(TrackingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
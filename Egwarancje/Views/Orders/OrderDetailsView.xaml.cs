using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views;

public partial class OrderDetailsView : ContentPage
{
	public OrderDetailsView(OrderDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views.Orders;

public partial class CartView : ContentPage
{
	public CartView(CartViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views;

public partial class CartView : ContentPage
{
	public CartView(CartViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
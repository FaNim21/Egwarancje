using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class OrderPanelView : ContentPage
{
	public OrderPanelView(OrderPanelViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
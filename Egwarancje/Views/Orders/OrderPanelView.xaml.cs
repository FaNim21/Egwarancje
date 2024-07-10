using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views.Orders;

public partial class OrderPanelView : ContentPage
{
	public OrderPanelView(OrderPanelViewModel viewModel)
	{
		InitializeComponent();
		//Application.Current.MainPage = new NavigationPage(this);
		BindingContext = viewModel;
	}
}
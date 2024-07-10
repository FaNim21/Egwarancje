using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views;

public partial class OrderPanelView : ContentPage
{
	public OrderPanelView(OrderPanelViewModel viewModel)
	{
		InitializeComponent();
		//Application.Current.MainPage = new NavigationPage(this);
		BindingContext = viewModel;
	}
}
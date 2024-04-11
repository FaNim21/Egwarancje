using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class OrderSpecsView : ContentPage
{
	public OrderSpecsView(OrderSpecsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
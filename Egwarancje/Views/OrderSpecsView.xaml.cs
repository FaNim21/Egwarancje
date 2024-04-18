using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class OrderSpecsView
{
	public OrderSpecsView(OrderSpecsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views.Orders;

public partial class ProductConfigurationView : ContentPage
{
	public ProductConfigurationView(ProductConfigurationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
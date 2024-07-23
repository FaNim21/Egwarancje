using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views.Orders;

public partial class ConfiguratorView : ContentPage
{
	public ConfiguratorView(ConfiguratorViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
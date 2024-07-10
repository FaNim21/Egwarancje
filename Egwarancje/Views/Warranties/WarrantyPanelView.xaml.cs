using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views;

public partial class WarrantyPanelView : ContentPage
{
	public WarrantyPanelView(WarrantyPanelViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class WarrantyPanelView : ContentPage
{
	public WarrantyPanelView(WarrantyPanelViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
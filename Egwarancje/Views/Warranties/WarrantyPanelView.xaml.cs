using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views.Warranties;

public partial class WarrantyPanelView : ContentPage
{
	public WarrantyPanelView(WarrantyPanelViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
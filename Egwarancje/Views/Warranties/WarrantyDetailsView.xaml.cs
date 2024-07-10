using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views;

public partial class WarrantyDetailsView
{
	public WarrantyDetailsView(WarrantyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
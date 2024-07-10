using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views.Warranties;

public partial class WarrantyDetailsView
{
	public WarrantyDetailsView(WarrantyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
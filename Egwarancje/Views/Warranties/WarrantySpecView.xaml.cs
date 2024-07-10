using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views.Warranties;

public partial class WarrantySpecView
{
	public WarrantySpecView(WarrantySpecViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
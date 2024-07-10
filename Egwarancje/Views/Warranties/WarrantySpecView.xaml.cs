using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views;

public partial class WarrantySpecView
{
	public WarrantySpecView(WarrantySpecViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
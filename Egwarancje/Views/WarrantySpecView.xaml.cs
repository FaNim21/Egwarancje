using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class WarrantySpecView
{
	public WarrantySpecView(WarrantySpecViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
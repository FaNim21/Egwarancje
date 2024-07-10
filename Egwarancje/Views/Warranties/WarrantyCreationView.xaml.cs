using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views;

public partial class WarrantyCreationView : ContentPage
{
	public WarrantyCreationView(WarrantyCreationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
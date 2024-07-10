using Egwarancje.ViewModels.Warranties;

namespace Egwarancje.Views.Warranties;

public partial class WarrantyCreationView : ContentPage
{
	public WarrantyCreationView(WarrantyCreationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
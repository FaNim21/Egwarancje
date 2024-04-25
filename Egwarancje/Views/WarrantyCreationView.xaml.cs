using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class WarrantyCreationView : ContentPage
{
	public WarrantyCreationView(WarrantyCreationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
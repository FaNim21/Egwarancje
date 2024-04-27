using Egwarancje.ViewModels;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.Views;

public partial class WarrantyDetailsView
{
	public WarrantyDetailsView(WarrantyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
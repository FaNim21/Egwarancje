using Egwarancje.ViewModels;

namespace Egwarancje.Views;

public partial class OrderRegistrationView : ContentPage
{
	public OrderRegistrationView(OrderRegistrationViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
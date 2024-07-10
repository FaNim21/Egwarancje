using Egwarancje.ViewModels.Orders;

namespace Egwarancje.Views.Orders;

public partial class OrderRegistrationView : ContentPage
{
    public OrderRegistrationView(OrderRegistrationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
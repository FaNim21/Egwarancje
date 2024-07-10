using Egwarancje.ViewModels.Orders;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.Views;

public partial class OrderSpecsView
{
    public OrderSpecsView(OrderSpecsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void CheckChanges(object sender, CheckedChangedEventArgs e)
    {
        if (sender == null) return;

        var checkBox = (CheckBox)sender;
        OrderSpec spec = (OrderSpec)checkBox.BindingContext;

        if (checkBox.IsChecked)
            ((OrderSpecsViewModel)BindingContext).AddOrderToWarranty(spec);
        else
            ((OrderSpecsViewModel)BindingContext).RemoveOrderFromWarranty(spec);
    }
}
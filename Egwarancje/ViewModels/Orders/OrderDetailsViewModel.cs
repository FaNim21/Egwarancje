using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Orders;

public partial class OrderDetailsViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty] private Order order;
    [ObservableProperty] private ObservableCollection<OrderSpec> orderSpecs;

    public OrderDetailsViewModel(UserService service, Order order)
    {
        this.service = service;
        this.Order = order;
        OrderSpecs = new ObservableCollection<OrderSpec>(order.OrderSpecs);
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}

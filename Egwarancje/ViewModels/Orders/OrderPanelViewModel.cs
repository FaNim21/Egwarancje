using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views.Orders;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;


namespace Egwarancje.ViewModels.Orders;

public partial class OrderPanelViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty]
    private ObservableCollection<Order> orders = [];


    public OrderPanelViewModel(UserService service)
    {
        this.service = service;

        if (service.User.Orders == null) return;
        Orders = new(service.User.Orders);
    }

    [RelayCommand]
    public async Task ShowOrderDetails(Order order)
    {
        await Shell.Current.Navigation.PushAsync(new OrderDetailsView(new OrderDetailsViewModel(service, order)));
    }
}

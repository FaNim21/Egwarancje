using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;


namespace Egwarancje.ViewModels;

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

    public async Task<Order?> AddOrder(Order order)
    {
        Order? dbOrder = await service.CreateOrderAsync(order);
        if (dbOrder is not null)
        {
            Orders.Add(dbOrder);
            service.User.Orders ??= [];
            service.User.Orders.Add(dbOrder);
        }
        return dbOrder;
    }

    public async Task<bool> AddOrderSpecs(OrderSpec orderSpec)
    {
        bool success = await service.CreateOrderSpecAsync(orderSpec);
        return success;
    }

    [RelayCommand]
    public async Task AddOrder()
    {
        await Shell.Current.Navigation.PushAsync(new OrderRegistrationView(new OrderRegistrationViewModel(this)));
    }

    [RelayCommand]
    public async Task ShowSpecs(Order order)
    {
        await MopupService.Instance.PushAsync(new OrderSpecsView(new OrderSpecsViewModel(service, this, order)));
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;


namespace Egwarancje.ViewModels;

public partial class OrderPanelViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private ObservableCollection<Order>? orders;


    public OrderPanelViewModel(LocalDatabaseContext database)
    {
        this.database = database;
        LoadOrders();
    }

    private void LoadOrders()
    {
        Orders = new(database.User!.Orders!);
    }

    public async Task AddOrder(Order order)
    {
        await database.Orders.AddAsync(order);
        Orders.Add(order);
        await database.SaveChangesAsync();
    }
    public async Task AddOrderSpecs(OrderSpec orderSpec)
    {
        await database.OrdersSpec.AddAsync(orderSpec);
        await database.SaveChangesAsync();
    }

    [RelayCommand]
    public async Task AddOrder()
    {
        await Shell.Current.Navigation.PushAsync(new OrderRegistrationView(new OrderRegistrationViewModel(this)));
    }

    [RelayCommand]
    public async Task RemoveOrder()
    {
        if (!Orders.Any()) return;
        var last = Orders?.Last();

        database.Orders.Remove(last);
        await database.SaveChangesAsync();
        Orders?.Remove(last);
    }

    [RelayCommand]
    public async Task ShowSpecs(Order order)
    {
        await MopupService.Instance.PushAsync(new OrderSpecsView(new OrderSpecsViewModel(database, order)));
    }
}

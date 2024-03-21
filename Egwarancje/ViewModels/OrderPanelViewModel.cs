using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class OrderPanelViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;

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

    [RelayCommand]
    public async Task AddOrder()
    {
        Random random = new();
        Order order = new()
        {
            UserId = 1,
            Comments = "",
            OrderSpecs = new(),
            OrderDate = DateTime.Now,
            OrderNumber = random.Next(5000, 99999),
        };

        await database.Orders.AddAsync(order);
        Orders?.Add(order);
        await database.SaveChangesAsync();
        //TODO: 2 Zrobic przechodzenie do view z rejestrowaniem zamowien (tam bedzie rejestrowanie po kodzie i NFC)
    }

    //TEMP
    [RelayCommand]
    public async Task RemoveOrder()
    {
        var last = Orders?.Last();
        if (last == null) return;

        database.Orders.Remove(last);
        await database.SaveChangesAsync();
        Orders?.Remove(last);
    }
}

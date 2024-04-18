using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class OrderSpecsViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;
    private readonly OrderPanelViewModel orderPanel;
    private readonly Order order;

    [ObservableProperty]
    private ObservableCollection<OrderSpec>? orderSpecs;

    public OrderSpecsViewModel(LocalDatabaseContext database, OrderPanelViewModel orderPanel, Order order)
    {
        this.database = database;
        this.orderPanel = orderPanel;
        this.order = order;

        if (order.OrderSpecs != null && order.OrderSpecs.Count > 0)
            OrderSpecs = new(order.OrderSpecs);
    }

    [RelayCommand]
    public void RemoveOrder()
    {
        //TODO: 0 trzeba zrobic kaskadowe usuwanie w relacjach albo samemu uwzglednic co chcemy usuwac czy cos poniewaz narazie w polaczeniu z gwarancjami nie mozna tak usuwac co jest wiadome

        for (int i = 0; i < order.OrderSpecs?.Count; i++)
            database.OrdersSpec.Remove(order.OrderSpecs[i]);
        database.Orders.Remove(order);

        database.SaveChanges();
        orderPanel.Orders.Remove(order);
        MopupService.Instance.PopAsync();
    }
}


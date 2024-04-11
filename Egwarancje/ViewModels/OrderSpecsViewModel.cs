using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class OrderSpecsViewModel : BaseViewModel
{
    private OrderPanelViewModel orderPanelViewModel;

    public OrderSpecsViewModel(OrderPanelViewModel orderPanelViewModel)
    {
        this.orderPanelViewModel = orderPanelViewModel;
    }

    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private ObservableCollection<OrderSpec>? orderSpecs;

    public OrderSpecsViewModel(LocalDatabaseContext database)
    {
        this.database = database;
        LoadSpecs();
    }

    private void LoadSpecs()
    {
        OrderSpecs = new(database.OrdersSpec!);
    }
}


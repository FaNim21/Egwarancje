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
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private ObservableCollection<OrderSpec>? orderSpecs;

    public OrderSpecsViewModel(LocalDatabaseContext database, Order order)
    {
        this.database = database;
        OrderSpecs = new(order.OrderSpecs);
    }
}


﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Orders;

public partial class OrderDetailsViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty] private Order order;
    [ObservableProperty] private ObservableCollection<OrderSpec> orderSpecs;
    [ObservableProperty] private ObservableCollection<string> shipmentStatuses = [];


    public OrderDetailsViewModel(UserService service, Order order)
    {
        this.service = service;
        Order = order;

        OrderSpecs = new ObservableCollection<OrderSpec>(order.OrderSpecs ?? []);

        //Temp
        shipmentStatuses.Add("Zamówienie w realizacji");
        shipmentStatuses.Add("Zamówienie zostało wysłane");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}

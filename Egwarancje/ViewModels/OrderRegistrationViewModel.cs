using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;
public partial class OrderRegistrationViewModel : BaseViewModel
{
    private OrderPanelViewModel orderPanelViewModel;

    [ObservableProperty]
    private string? comments;

    [ObservableProperty]
    private int orderNumber;

    public OrderRegistrationViewModel(OrderPanelViewModel orderPanelViewModel)
    {
        this.orderPanelViewModel = orderPanelViewModel;
    }

    [RelayCommand]
    public async Task AddOrder()
    {
        if (string.IsNullOrEmpty(orderNumber.ToString()))
        {
            await Application.Current!.MainPage!.DisplayAlert("Brak numeru zamówienia", "Uzupełnij numer zamówienia", "OK");
            return;
        }

        Order order = new()
        {
            UserId = orderPanelViewModel.database.User.Id,
            Comments = comments,
            OrderSpecs = [],
            OrderDate = DateTime.Now,
            OrderNumber = orderNumber,
        };

        await orderPanelViewModel.AddOrder(order);

        OrderSpec orderSpec1 = new()
        {
            OrderId = order.Id,
            Name = "krzesło",
            Realization = "drewno dębowe",
            ValueNet = 100.0f,
            ValueGross = 123.0f,
            WarrantyLength = 30,
            Order = order
        };

        OrderSpec orderSpec2 = new()
        {
            OrderId = order.Id,
            Name = "stół",
            Realization = "drewno dębowe",
            ValueNet = 200.0f,
            ValueGross = 246.0f,
            WarrantyLength = 30,
            Order = order
        };

        OrderSpec orderSpec3 = new()
        {
            OrderId = order.Id,
            Name = "krzesło",
            Realization = "drewno dębowe",
            ValueNet = 100.0f,
            ValueGross = 123.0f,
            WarrantyLength = 30,
            Order = order
        };

        OrderSpec orderSpec4 = new()
        {
            OrderId = order.Id,
            Name = "lampka",
            Realization = "szkło",
            ValueNet = 20.0f,
            ValueGross = 24.6f,
            WarrantyLength = 14,
            Order = order
        };

        //order.OrderSpecs.Add(orderSpec);

        await orderPanelViewModel.AddOrderSpecs(orderSpec1);
        await orderPanelViewModel.AddOrderSpecs(orderSpec2);
        await orderPanelViewModel.AddOrderSpecs(orderSpec3);
        await orderPanelViewModel.AddOrderSpecs(orderSpec4);

        await Application.Current!.MainPage!.DisplayAlert("Message", "Pomyślnie dodano zamówienie", "OK");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

}

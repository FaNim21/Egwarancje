using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Egwarancje.ViewModels;

public partial class OrderSpecsViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;
    private readonly OrderPanelViewModel orderPanel;
    private readonly Order order;

    [ObservableProperty] private ObservableCollection<OrderSpec>? orderSpecs;
    private List<OrderSpec> _warrantySpecs = [];

    public OrderSpecsViewModel(LocalDatabaseContext database, OrderPanelViewModel orderPanel, Order order)
    {
        this.database = database;
        this.orderPanel = orderPanel;
        this.order = order;

        if (order.OrderSpecs != null && order.OrderSpecs.Count > 0)
            OrderSpecs = new(order.OrderSpecs);
    }

    [RelayCommand]
    public async Task CreateWarranty()
    {
        await MopupService.Instance.PopAsync();
        await Shell.Current.Navigation.PushAsync(new WarrantyCreationView(new WarrantyCreationViewModel(database, order, _warrantySpecs)));
        //await Shell.Current.GoToAsync("///MainTab//WarrantyCreation");
    }

    [RelayCommand]
    public async Task RemoveOrder()
    {
        //TODO: 0 trzeba zrobic kaskadowe usuwanie w relacjach albo samemu uwzglednic co chcemy usuwac czy cos poniewaz narazie w polaczeniu z gwarancjami nie mozna tak usuwac co jest wiadome

        Warranty[] warranties = [.. database.Warranties
                                .Where(w => w.Order.UserId == database.User!.Id)
                                .Include(w => w.WarrantySpecs)
                                .ThenInclude(w => w.Attachments)];

        for (int i = 0; i < warranties.Length; i++)
        {
            var warranty = warranties[i];

            for (int j = 0; j < warranty.WarrantySpecs.Count; j++)
            {
                var spec = warranty.WarrantySpecs[j];

                if (spec.Attachments == null || spec.Attachments.Count == 0) continue;
                for (int k = 0; k < spec.Attachments.Count; k++)
                {
                    var attachment = spec.Attachments[k];
                    database.Attachments.Remove(attachment);
                    await database.SaveChangesAsync();
                }
            }

            database.Warranties.Remove(warranty);
            await database.SaveChangesAsync();
        }

        for (int i = 0; i < order.OrderSpecs?.Count; i++)
            database.OrdersSpec.Remove(order.OrderSpecs[i]);
        database.Orders.Remove(order);

        await database.SaveChangesAsync();
        orderPanel.Orders.Remove(order);
        await MopupService.Instance.PopAsync();
    }

    public void AddOrderToWarranty(OrderSpec spec)
    {
        Trace.WriteLine($"Dodano: {spec.Name}");
        _warrantySpecs.Add(spec);
    }

    public void RemoveOrderFromWarranty(OrderSpec spec)
    {
        Trace.WriteLine($"Usunieto: {spec.Name}");
        _warrantySpecs.Remove(spec);
    }
}


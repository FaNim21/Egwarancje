using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.ViewModels.Warranties;
using Egwarancje.Views.Warranties;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Egwarancje.ViewModels.Orders;

public partial class OrderSpecsViewModel : BaseViewModel
{
    private readonly UserService service;
    private readonly Order order;
    private readonly WarrantyCreationViewModel warrantyCreationViewModel;

    [ObservableProperty] private ObservableCollection<OrderSpec>? orderSpecs;
    private List<OrderSpec> _warrantySpecs = [];

    public OrderSpecsViewModel(UserService service, WarrantyCreationViewModel warrantyCreationViewModel, Order order)
    {
        this.service = service;
        this.order = order;
        this.warrantyCreationViewModel = warrantyCreationViewModel;

        if (order.OrderSpecs != null && order.OrderSpecs.Count > 0)
            OrderSpecs = new(order.OrderSpecs);
    }

    [RelayCommand]
    public async Task CreateWarranty()
    {
        if (_warrantySpecs == null || _warrantySpecs.Count == 0) return;

        //warrantyCreationViewModel.WarrantySpecs = new(_warrantySpecs ?? []);
        await MopupService.Instance.PopAsync();
        //await Shell.Current.Navigation.PushAsync(new WarrantyCreationView(new WarrantyCreationViewModel(service, order, _warrantySpecs)));
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


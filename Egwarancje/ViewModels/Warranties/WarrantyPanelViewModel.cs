using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views.Warranties;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Warranties;

public partial class WarrantyPanelViewModel : BaseViewModel
{
    public readonly UserService service;

    [ObservableProperty]
    private ObservableCollection<Warranty> warranties = [];


    public WarrantyPanelViewModel(UserService service)
    {
        this.service = service;

        if (service.User.Warranties == null) return;
        warranties = new(service.User.Warranties);
    }

    [RelayCommand]
    public async Task ShowDetails(Warranty warranty)
    {
        await MopupService.Instance.PushAsync(new WarrantyDetailsView(new WarrantyDetailsViewModel(warranty)));
    }

    [RelayCommand]
    public async Task CreateWarranty()
    {
        await Shell.Current.Navigation.PushAsync(new WarrantyCreationView(new WarrantyCreationViewModel(service)));
    }
}


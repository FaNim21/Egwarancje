using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Utils;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Orders;

public partial class ProductConfigurationViewModel : BaseViewModel
{
    [ObservableProperty] private ProductConfigurator _furniture;
    [ObservableProperty] private ObservableCollection<Resources> _resources=[];

    public ProductConfigurationViewModel(ProductConfigurator furniture)
    {
        _furniture = furniture;
        Resources = new ObservableCollection<Resources>(furniture.Structure?.Resources ?? new Resources[0]);
    }

    [RelayCommand]
    public async Task SaveProduct()
    {
        // Tutaj zrobic dodawanie do koszyka skonfigurowanego produktu
        await Application.Current!.MainPage!.DisplayAlert("Zapisano", $"Konfiguracja dla {Furniture.Name} została zapisana.", "OK");
        await Shell.Current.GoToAsync("///Configurator");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///OrderPanel");
    }
}

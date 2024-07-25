using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Orders;

public partial class ProductConfigurationViewModel : BaseViewModel
{
    private readonly ConfiguratorViewModel _configurator;

    [ObservableProperty] private string _chosenMaterial; 
    [ObservableProperty] private string _chosenColor; 

    [ObservableProperty] private ProductConfigurator _furniture;
    [ObservableProperty] private ObservableCollection<Resources> _resources = [];


    public ProductConfigurationViewModel(ConfiguratorViewModel configurator, ProductConfigurator furniture)
    {
        _furniture = furniture;
        _configurator = configurator;
        Resources = new ObservableCollection<Resources>(furniture.Structure?.Resources ?? []);
    }

    [RelayCommand]
    public async Task SaveProduct()
    {
        // Tutaj zrobic dodawanie do koszyka skonfigurowanego produktu
        await Application.Current!.MainPage!.DisplayAlert("Zapisano", $"Konfiguracja dla {Furniture.Name} została zapisana.", "OK");

        await Back();
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }
}

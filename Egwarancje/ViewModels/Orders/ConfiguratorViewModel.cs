using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views.Orders;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Egwarancje.ViewModels.Orders;

public partial class ConfiguratorViewModel : BaseViewModel
{
    private readonly UserService _userService;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    [ObservableProperty] private ObservableCollection<ProductConfigurator> _furnitures = [];


    public ConfiguratorViewModel(UserService userService)
    {
        _userService = userService;
        
        _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        Task.Run(LoadProducts);
    }

    private async Task LoadProducts()
    {
        Furnitures.Clear();
        var products = await _userService.GetAllProducts();
        if (products == null) return;
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };


        for (int i = 0; i < products.Count; i++)
        {
            var product = products[i];
            if (product.Structure == null) continue;

            ProductStructure productStructure = JsonSerializer.Deserialize<ProductStructure>(product.Structure, options);
            Furnitures.Add(new ProductConfigurator() { ImagePath = product.ImagePath, Name = product.Name, Structure = productStructure });
        }
    }

    [RelayCommand]
    public async Task ProductConfiguration(ProductConfigurator productConfigurator)
    {
        await Shell.Current.Navigation.PushAsync(new ProductConfigurationView(new ProductConfigurationViewModel(productConfigurator)));
    }

}

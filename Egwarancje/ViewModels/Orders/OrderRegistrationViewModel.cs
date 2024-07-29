using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary.Models;
using System.Text.Json;

namespace Egwarancje.ViewModels.Orders;
public partial class OrderRegistrationViewModel : BaseViewModel
{
    private readonly CartViewModel _cartViewModel;
    private readonly Random random;

    [ObservableProperty]
    private string? comment;


    public OrderRegistrationViewModel(CartViewModel cartViewModel)
    {
        _cartViewModel = cartViewModel;
        random = new Random();
    }

    [RelayCommand]
    public async Task AddOrder()
    {
        Order? dbOrder = await _cartViewModel.AddOrder();
        if (dbOrder is null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Problem w dodaniu zamówienia", "OK");
            return;
        }

        dbOrder.Comments = Comment;
        dbOrder.OrderSpecs ??= [];
        for (int i = 0; i < _cartViewModel.Products.Count; i++)
        {
            var product = _cartViewModel.Products[i];
            string details = JsonSerializer.Serialize(product.Details);

            OrderSpec spec = new()
            {
                OrderId = dbOrder.Id,
                Name = product.Name,
                Realization = details,
                ValueNet = MathF.Round((float)(random.NextDouble() * 1000.0D) * 100f) / 100f,
                ValueGross = MathF.Round((float)(random.NextDouble() * 1000.0D) * 100f) / 100f,
                WarrantyLength = random.Next(100),
                Order = dbOrder
            };

            bool success = await _cartViewModel.AddOrderSpecs(spec);
            if (!success)
            {
                await Application.Current!.MainPage!.DisplayAlert("Message", "Problem w dodaniu specyfikacji dla zamówienia", "OK");
                return;
            }
            dbOrder.OrderSpecs.Add(spec);
        }

        await _cartViewModel.ClearProducts();

        await Application.Current!.MainPage!.DisplayAlert("Message", "Pomyślnie dodano zamówienie", "OK");
        await Back();
        await Shell.Current.GoToAsync("///MainTab//OrderPanel");
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

}

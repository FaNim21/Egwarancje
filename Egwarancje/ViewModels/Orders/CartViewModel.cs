using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Egwarancje.ViewModels.Orders;

public partial class CartViewModel : BaseViewModel
{
    private readonly UserService _userService;

    [ObservableProperty] private ObservableCollection<CartProduct> _products = [];


    public CartViewModel(UserService userService)
    {
        _userService = userService;
        Task.Run(LoadOrCreateCart);
    }

    public async Task LoadOrCreateCart()
    {
        if (_userService.User.Cart == null)
        {
            Cart newCart = new()
            {
                UserId = _userService.User.Id,
            };

            Cart? cart = await _userService.CreateCart(newCart);
            if (cart == null) return; 
            _userService.User.Cart = cart;
        }

        string? cartProductContent = _userService.User.Cart.Products;
        if (string.IsNullOrEmpty(cartProductContent)) return;

        CartProduct[]? products = JsonSerializer.Deserialize<CartProduct[]>(cartProductContent);
        if (products == null) return;

        Products = new(products);
    }

    [RelayCommand]
    public async Task DeleteFromCart(CartProduct product)
    {
        //TODO: 0 Zrobic usuwanie produktow z koszyka
    }
}

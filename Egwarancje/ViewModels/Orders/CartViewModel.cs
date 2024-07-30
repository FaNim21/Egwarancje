using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Services;
using Egwarancje.Views.Orders;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace Egwarancje.ViewModels.Orders;

public partial class CartViewModel : BaseViewModel
{
    private readonly UserService _userService;

    [ObservableProperty] private ObservableCollection<CartProduct> _products = [];

    [ObservableProperty] public bool _isCartEmpty = true;


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
            if (cart == null)
            {
                IsCartEmpty = true;
                return;
            }
            _userService.User.Cart = cart;
            IsCartEmpty = false;
        }

        string? cartProductContent = _userService.User.Cart.Products;
        if (string.IsNullOrEmpty(cartProductContent)) return;

        CartProduct[]? products = JsonSerializer.Deserialize<CartProduct[]>(cartProductContent);
        if (products == null) return;

        Products.Clear();
        Products = new(products);
    }

    public async Task<Order?> AddOrder()
    {
        Order order = new()
        {
            UserId = _userService.User.Id,
            User = _userService.User,
        };
        Order? dbOrder = await _userService.CreateOrderAsync(order);
        if (dbOrder is not null)
        {
            dbOrder.OrderNumber = order.Id;
            _userService.User.Orders ??= [];
            _userService.User.Orders.Add(dbOrder);
        }
        return dbOrder;
    }

    public async Task<bool> AddOrderSpecs(OrderSpec orderSpec)
    {
        return await _userService.CreateOrderSpecAsync(orderSpec);
    }

    public async Task ClearProducts()
    {
        bool success = await _userService.DeleteCartAsync(_userService.User.Cart!);
        if (!success) return;

        _userService.User.Cart = null;
        await LoadOrCreateCart();
    }

    [RelayCommand]
    public async Task DeleteFromCart(CartProduct product)
    {
        Products.Remove(product);
        string json = JsonSerializer.Serialize(Products);
        _userService.User.Cart!.Products = json;
        await _userService.UpdateCartAsync(_userService.User.Cart);
    }

    [RelayCommand]
    public async Task SubmitOrder()
    {
        if (Products.Count == 0) return;
        await Shell.Current.Navigation.PushAsync(new OrderRegistrationView(new OrderRegistrationViewModel(this)));
    }
}

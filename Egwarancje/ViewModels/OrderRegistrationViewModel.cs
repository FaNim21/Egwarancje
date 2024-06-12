using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Utils;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.ViewModels;
public partial class OrderRegistrationViewModel : BaseViewModel
{
    private readonly OrderPanelViewModel orderPanelViewModel;

    [ObservableProperty]
    private string? orderNumber;


    public OrderRegistrationViewModel(OrderPanelViewModel orderPanelViewModel)
    {
        this.orderPanelViewModel = orderPanelViewModel;
    }

    [RelayCommand]
    public async Task AddOrder()
    {
        if (string.IsNullOrEmpty(OrderNumber))
        {
            await Application.Current!.MainPage!.DisplayAlert("Brak numeru zamówienia", "Uzupełnij numer zamówienia", "OK");
            return;
        }

        OrderGenerator generator = new(int.Parse(OrderNumber));
        Order order = generator.GenerateOrder(orderPanelViewModel.service.User);
        Order? dbOrder = await orderPanelViewModel.AddOrder(order);
        if (dbOrder is null)
        {
            await Application.Current!.MainPage!.DisplayAlert("Message", "Problem w dodaniu zamówienia", "OK");
            return;
        }

        dbOrder.OrderSpecs ??= [];
        OrderSpec[] specs = generator.GenerateOrderSpecs(dbOrder);
        for (int i = 0; i < specs.Length; i++)
        {
            bool success = await orderPanelViewModel.AddOrderSpecs(specs[i]);
            if (!success)
            {
                await Application.Current!.MainPage!.DisplayAlert("Message", "Problem w dodaniu specyfikacji dla zamówienia", "OK");
                return;
            }
            dbOrder.OrderSpecs.Add(specs[i]);
        }

        await Application.Current!.MainPage!.DisplayAlert("Message", "Pomyślnie dodano zamówienie", "OK");
        await Shell.Current.Navigation.PopAsync(); //Po dodaniu zamowienia powrot do panelu zamowien
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

}

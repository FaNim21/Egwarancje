using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Utils;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.ViewModels;
public partial class OrderRegistrationViewModel : BaseViewModel
{
    private readonly OrderPanelViewModel orderPanelViewModel;

    [ObservableProperty]
    private string? comments;

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
        Order order = generator.GenerateOrder(orderPanelViewModel.database.User!);
        await orderPanelViewModel.AddOrder(order);

        OrderSpec[] specs = generator.GenerateOrderSpecs(order);
        for (int i = 0; i < specs.Length; i++)
            await orderPanelViewModel.AddOrderSpecs(specs[i]);

        await Application.Current!.MainPage!.DisplayAlert("Message", "Pomyślnie dodano zamówienie", "OK");
        await Shell.Current.Navigation.PopAsync(); //Po dodaniu zamowienia powrot do panelu zamowien
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.Navigation.PopAsync();
    }

}

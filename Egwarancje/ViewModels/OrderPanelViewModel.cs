using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class OrderPanelViewModel : BaseViewModel
{
    [ObservableProperty]
    private ObservableCollection<string> orders;


    public OrderPanelViewModel()
    {
        
    }

    [RelayCommand]
    public async Task AddOrder()
    {
        //TODO: 2 Zrobic przechodzenie do view z rejestrowaniem zamowien (tam bedzie rejestrowanie po kodzie i NFC)
    }
}

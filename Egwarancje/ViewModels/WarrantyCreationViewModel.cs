using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantyCreationViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private string? comment;

    [ObservableProperty]
    private Order? order;

    [ObservableProperty]
    private WarrantyStatusType? status;

    [ObservableProperty]
    private DateTime? dateOfWarranty;

    public WarrantyCreationViewModel(LocalDatabaseContext database)
    {
        this.database = database;
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("///OrderPanel");
    }
}

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantyPanelViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;

    [ObservableProperty]
    private ObservableCollection<Warranty> warranties;


    public WarrantyPanelViewModel(LocalDatabaseContext database)
    {
        this.database = database;

        Warranties = new(database.Warranties
                                .Where(w => w.Order.UserId == database.User!.Id)
                                .Include(w => w.WarrantySpecs)
                                .ThenInclude(w => w.Attachments));
    }

    [RelayCommand]
    public async Task ShowDetails(Warranty warranty)
    {
        await MopupService.Instance.PushAsync(new WarrantyDetailsView(new WarrantyDetailsViewModel(warranty)));
    }
}


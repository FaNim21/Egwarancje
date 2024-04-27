using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Egwarancje.ViewModels;

public partial class WarrantyDetailsViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;
    private readonly Warranty warranty;
    private readonly WarrantyPanelViewModel warrantyPanel;

    [ObservableProperty] private ObservableCollection<WarrantySpec>? warrantySpecs;

    public WarrantyDetailsViewModel(LocalDatabaseContext database, WarrantyPanelViewModel warrantyPanel, Warranty warranty)
    {
        this.database = database;
        this.warrantyPanel = warrantyPanel;
        this.warranty = warranty;

        if (warranty.WarrantySpecs != null && warranty.WarrantySpecs.Count > 0)
            WarrantySpecs = new(warranty.WarrantySpecs);
    }

    [RelayCommand]
    public async Task ShowSpec(WarrantySpec spec)
    {
        //await MopupService.Instance.PushAsync(new WarrantySpecView(new WarrantySpecViewModel(database, this, warrantySpecs)));
        return;
    }
}

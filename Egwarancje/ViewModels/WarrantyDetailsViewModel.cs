using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Views;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Egwarancje.ViewModels;

public partial class WarrantySpecDetail : BaseViewModel
{
    [ObservableProperty] private bool isVisible;
    [ObservableProperty] private WarrantySpec spec;

    public WarrantySpecDetail(WarrantySpec spec)
    {
        this.spec = spec;
    }
}

public partial class WarrantyDetailsViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;
    private readonly Warranty warranty;
    private readonly WarrantyPanelViewModel warrantyPanel;

    [ObservableProperty] private ObservableCollection<WarrantySpecDetail> warrantySpecs = [];

    public WarrantyDetailsViewModel(LocalDatabaseContext database, WarrantyPanelViewModel warrantyPanel, Warranty warranty)
    {
        this.database = database;
        this.warrantyPanel = warrantyPanel;
        this.warranty = warranty;

        if (warranty.WarrantySpecs != null && warranty.WarrantySpecs.Count > 0)
        {
            for (int i = 0; i < warranty.WarrantySpecs.Count; i++)
            {
                var current = warranty.WarrantySpecs[i];
                WarrantySpecDetail specDetail = new(current);
                warrantySpecs.Add(specDetail);
            }
        }
    }

    [RelayCommand]
    public void ShowDetails(WarrantySpecDetail specDetail)
    {
        foreach (var spec in WarrantySpecs)
        {
            spec.IsVisible = false;
        }
        specDetail.IsVisible = true;
    }
}

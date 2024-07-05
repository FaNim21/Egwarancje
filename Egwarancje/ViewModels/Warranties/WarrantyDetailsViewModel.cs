using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Warranties;

public partial class WarrantyDetailsViewModel : BaseViewModel
{
    private readonly Warranty warranty;

    [ObservableProperty] private ObservableCollection<WarrantySpecDetail> warrantySpecs = [];

    public WarrantyDetailsViewModel(Warranty warranty)
    {
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
        for (int i = 0; i < WarrantySpecs.Count; i++)
        {
            if (WarrantySpecs[i] == specDetail) continue;
            WarrantySpecs[i].IsVisible = false;
        }

        specDetail.IsVisible = !specDetail.IsVisible;
    }

    [RelayCommand]
    public async Task Close()
    {
        await MopupService.Instance.PopAsync();
    }
}

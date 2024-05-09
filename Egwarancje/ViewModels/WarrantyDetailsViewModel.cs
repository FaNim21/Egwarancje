using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantySpecDetail : BaseViewModel
{
    [ObservableProperty] private bool isVisible;
    [ObservableProperty] private WarrantySpec spec;
    [ObservableProperty] private ObservableCollection<WarrantySpecAttachmentViewModel> attachments = [];

    public WarrantySpecDetail(WarrantySpec spec)
    {
        this.spec = spec;

        if (spec.Attachments == null || spec.Attachments.Count == 0) return;
        for (int i = 0; i < spec.Attachments.Count; i++)
        {
            var current = spec.Attachments[i];
            WarrantySpecAttachmentViewModel attachmentViewModel = new(current);
            attachments.Add(attachmentViewModel);
        }
    }
}

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

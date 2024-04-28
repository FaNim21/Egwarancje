using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantySpecDetail : BaseViewModel, IDisposable
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

    public void Dispose()
    {
        foreach (var attachment in Attachments)
            attachment.Dispose();
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

    [RelayCommand]
    public async Task Close()
    {
        foreach (var spec in WarrantySpecs)
            spec.Dispose();

        await MopupService.Instance.PopAsync();
    }
}

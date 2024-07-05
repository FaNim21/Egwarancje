using CommunityToolkit.Mvvm.ComponentModel;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Warranties;

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

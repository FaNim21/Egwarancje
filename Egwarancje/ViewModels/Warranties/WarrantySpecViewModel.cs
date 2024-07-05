using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels.Warranties;

public partial class WarrantySpecViewModel : BaseViewModel
{
    private readonly WarrantySpec warrantySpec;

    [ObservableProperty] private string comment;
    [ObservableProperty] private ObservableCollection<WarrantySpecAttachmentViewModel> attachments = [];


    public WarrantySpecViewModel(WarrantySpec warrantySpec)
    {
        this.warrantySpec = warrantySpec;

        Comment = warrantySpec.Comments;
        if (warrantySpec.Attachments == null || warrantySpec.Attachments.Count == 0) return;
        for (int i = 0; i < warrantySpec.Attachments.Count; i++)
        {
            var current = warrantySpec.Attachments[i];
            WarrantySpecAttachmentViewModel attachmentViewModel = new(current);
            attachments.Add(attachmentViewModel);
        }
    }

    [RelayCommand]
    public async Task SaveChanges()
    {
        warrantySpec.Comments = Comment;
        warrantySpec.Attachments = [];

        for (int i = 0; i < Attachments.Count; i++)
        {
            var current = Attachments[i];
            warrantySpec.Attachments.Add(current.Attachment);
        }

        await MopupService.Instance.PopAsync();
        Attachments.Clear();
    }

    [RelayCommand]
    public async Task UseCamera()
    {
        if (!MediaPicker.Default.IsCaptureSupported) return;

        FileResult? file = await MediaPicker.Default.CapturePhotoAsync();
        if (file == null) return;

        CreateAttachment(file.FullPath);
    }

    [RelayCommand]
    public async Task GetMedia()
    {
        if (!MediaPicker.Default.IsCaptureSupported) return;

        FileResult? file = await MediaPicker.Default.PickPhotoAsync();
        if (file == null) return;

        CreateAttachment(file.FullPath);
    }

    private Attachment CreateAttachment(string imagePath)
    {
        Attachment attachment = new()
        {
            ImagePath = imagePath,
            WarrantySpec = warrantySpec
        };

        Attachments.Add(new WarrantySpecAttachmentViewModel(attachment));
        return attachment;
    }
}

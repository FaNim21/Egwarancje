using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;

namespace Egwarancje.ViewModels;

public partial class WarrantySpecAttachmentViewModel : BaseViewModel
{
    [ObservableProperty] private Attachment attachment;
    [ObservableProperty] private ImageSource image;


    public WarrantySpecAttachmentViewModel(Attachment attachment)
    {
        this.attachment = attachment;

        Image = ImageSource.FromFile(attachment.ImagePath);
    }
}

public partial class WarrantySpecViewModel : BaseViewModel
{
    private readonly LocalDatabaseContext database;
    private readonly WarrantySpec warrantySpec;

    [ObservableProperty] private string comment;
    [ObservableProperty] private ObservableCollection<WarrantySpecAttachmentViewModel> attachments = [];


    public WarrantySpecViewModel(LocalDatabaseContext database, WarrantySpec warrantySpec)
    {
        this.database = database;
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

    // Maksymalna długość komentarza 250 znaków
    partial void OnCommentChanged(string value)
    {
        if (value.Length > 250)
        {
            comment = value.Substring(0, 250);
            OnPropertyChanged(nameof(Comment));
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

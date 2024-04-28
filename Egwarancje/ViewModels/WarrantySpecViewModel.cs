using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Egwarancje.Utils;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using Mopups.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Egwarancje.ViewModels;

public partial class WarrantySpecAttachmentViewModel : BaseViewModel, IDisposable
{
    private readonly MemoryStream _memoryStream;

    [ObservableProperty] private Attachment attachment;
    [ObservableProperty] private ImageSource image;


    public WarrantySpecAttachmentViewModel(Attachment attachment)
    {
        this.attachment = attachment;

        byte[] imageDataCopy = (byte[])attachment.Image!.Clone();
        _memoryStream = new(imageDataCopy);
        Image = ImageSource.FromStream(() => _memoryStream);
    }

    public void Dispose()
    {
        _memoryStream.Dispose();
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

        foreach (var attachment in Attachments)
            attachment.Dispose();

        await MopupService.Instance.PopAsync();
        Attachments.Clear();
    }

    [RelayCommand]
    public async Task UseCamera()
    {
        if (!MediaPicker.Default.IsCaptureSupported) return;

        FileResult? file = await MediaPicker.Default.CapturePhotoAsync();
        if (file == null) return;

        CreateAttachment(File.ReadAllBytes(file.FullPath));
    }

    [RelayCommand]
    public async Task GetMedia()
    {
        if (!MediaPicker.Default.IsCaptureSupported) return;

        FileResult? file = await MediaPicker.Default.PickPhotoAsync();
        if (file == null) return;

        CreateAttachment(File.ReadAllBytes(file.FullPath));
    }

    private Attachment CreateAttachment(byte[] image)
    {
        try
        {
            image = Helper.CompressImage(image, 15);
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }

        Attachment attachment = new()
        {
            Image = image,
            WarrantySpec = warrantySpec
        };

        Attachments.Add(new WarrantySpecAttachmentViewModel(attachment));
        return attachment;
    }
}

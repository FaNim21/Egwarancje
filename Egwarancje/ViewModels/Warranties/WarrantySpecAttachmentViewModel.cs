using CommunityToolkit.Mvvm.ComponentModel;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.ViewModels.Warranties;

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

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;

namespace Egwarancje.ViewModels;

public partial class WarrantyCreationViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;
    public readonly Order order;

    public Warranty warranty;
    public List<WarrantySpec> warrantySpecs = [];

    [ObservableProperty] private string? comment;
    [ObservableProperty] private WarrantyStatusType? status;
    [ObservableProperty] private DateTime dateOfWarranty;


    public WarrantyCreationViewModel(LocalDatabaseContext database, Order order, List<OrderSpec> orderSpecs)
    {
        this.database = database;
        this.order = order;

        warranty = new()
        {
            DateOfWarranty = DateOfWarranty,
            Status = WarrantyStatusType.Awaitng,
            OrderId = order.Id,
            Order = order!,
        };

        for (int i = 0; i < orderSpecs.Count; i++)
        {
            var currentOrderSpec = warrantySpecs[i];

            WarrantySpec warrantySpec = new()
            {
                WarrantyId = warranty.Id,
                OrderSpecId = currentOrderSpec.Id,
            };
            warrantySpecs.Add(warrantySpec);
        }

        dateOfWarranty = DateTime.Now;
    }

    [RelayCommand]
    public async Task Back()
    {
        //TODO: 0 tutaj dodac popup potwiedzenia czy na pewno chcesz wyjsc

        await Shell.Current.Navigation.PopAsync();
    }

    public async Task Confirm()
    {
        //TODO: 0 Tutaj walidacja i potwierdzenie zrobienia gwarancji

        warranty.Comments = Comment;
        database.Warranties.Add(warranty);

        foreach (var warrantySpec in warrantySpecs)
        {
            if (warrantySpec.Attachments != null && warrantySpec.Attachments.Count != 0)
                foreach (var attachment in warrantySpec.Attachments)
                    database.Attachments.Add(attachment);

            database.WarrantiesSpecs.Add(warrantySpec);
        }

        //TODO: 0 to tutaj dzieje sie w popupie dla warranty speca
        /*List<Attachment> attachments = [];
        for (int i = 0; i < orderSpecs.Count; i++)
        {
            Attachment attachment = new();
            attachment.WarrantySpecId = warrantySpec.Id;
            attachment.WarrantySpec = warrantySpec
            attachment.image = null; //Tu wiadomo przekazujemy obrazek

            attachments.Add(attachment);
            database.Attachments.Add(attachment);
        }*/

        database.SaveChanges();
        await Shell.Current.Navigation.PopAsync();
    }
}

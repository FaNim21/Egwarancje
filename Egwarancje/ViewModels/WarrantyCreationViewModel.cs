using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EgwarancjeDbLibrary;
using EgwarancjeDbLibrary.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Egwarancje.ViewModels;

public partial class WarrantyCreationViewModel : BaseViewModel
{
    public readonly LocalDatabaseContext database;
    public readonly Order order;

    public Warranty warranty;

    [ObservableProperty] private ObservableCollection<WarrantySpec> warrantySpecs = [];
    [ObservableProperty] private string? comment;
    [ObservableProperty] private WarrantyStatusType? status;
    [ObservableProperty] private DateTime dateOfWarranty;


    public WarrantyCreationViewModel(LocalDatabaseContext database, Order order, List<OrderSpec> orderSpecs)
    {
        this.database = database;
        this.order = order;

        dateOfWarranty = DateTime.Now;

        warranty = new()
        {
            DateOfWarranty = DateOfWarranty,
            Status = WarrantyStatusType.Awaitng,
            OrderId = order.Id,
            Order = order!,
        };

        for (int i = 0; i < orderSpecs.Count; i++)
        {
            var currentOrderSpec = orderSpecs[i];

            WarrantySpec warrantySpec = new()
            {
                OrderSpecId = currentOrderSpec.Id,
                OrderSpec = currentOrderSpec,
                Comments = ""
            };
            WarrantySpecs.Add(warrantySpec);
        }
    }

    [RelayCommand]
    public async Task Back()
    {
        //TODO: 0 tutaj dodac popup potwiedzenia czy na pewno chcesz wyjsc

        await Shell.Current.Navigation.PopAsync();
    }

    [RelayCommand]
    public async Task Confirm()
    {
        //TODO: 0 Tutaj walidacja i potwierdzenie zrobienia gwarancji

        warranty.Comments = Comment;
        await database.Warranties.AddAsync(warranty);
        await database.SaveChangesAsync();

        for (int i = 0; i < WarrantySpecs.Count; i++)
        {
            try
            {
                var current = WarrantySpecs[i];
                current.WarrantyId = warranty.Id;
                current.Warranty = warranty;

                if (current.Attachments != null && current.Attachments.Count != 0)
                    foreach (var attachment in current.Attachments)
                        await database.Attachments.AddAsync(attachment);

                await database.WarrantiesSpecs.AddAsync(current);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }

            await database.SaveChangesAsync();
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
        await Application.Current!.MainPage!.DisplayAlert("Dodano", "Pomyślnie zgłoszono gwarancję", "OK");
        await Shell.Current.Navigation.PopAsync();
    }
}
